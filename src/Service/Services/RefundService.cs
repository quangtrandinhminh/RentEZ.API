using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Net.payOS;
using Net.payOS.Types;
using Repository.Base;
using Repository.Infrastructure;
using Repository.Interfaces;
using Repository.Models;
using Repository.Models.Identity;
using Serilog;
using Service.ApiModels.Refund;
using Service.ApiModels.VNPay;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utility.Config;
using Utility.Constants;
using Utility.Enum;
using Utility.Exceptions;
using Utility.Helpers;
using Utility.Helpers.VnPay;

namespace Service.Services
{
    public class RefundService : IRefundService
    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<OrderShop> _orderShopRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RefundService(
            IBaseRepository<Order> orderRepository,
            IBaseRepository<OrderShop> orderShopRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _orderShopRepository = orderShopRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<(string customerRefundQR, string shopPaymentQR)> ProcessRefundAsync(RefundRequest request)
        {
            var order = await _orderRepository.GetAllWithCondition()
                .Include(x => x.Customer)
                .Include(x => x.OrderShops)
                    .ThenInclude(x => x.Shop)
                .FirstOrDefaultAsync(o => o.Id == request.OrderId);

            if (order == null)
                throw new AppException(ResponseCodeConstants.NOT_FOUND,
                    ResponseMessageConstrantsOrder.NOT_FOUND,
                    StatusCodes.Status404NotFound);

            var admin = await _userRepository.GetSingleAsync(u => u.Id == request.UserId);
            if (admin == null)
                throw new AppException(ResponseCodeConstants.NOT_FOUND,
                    "Admin not found",
                    StatusCodes.Status404NotFound);

            var orderShop = order.OrderShops
                .FirstOrDefault(os => os.ShopId == request.ShopId);

            if (orderShop == null)
                throw new AppException(ResponseCodeConstants.NOT_FOUND,
                    "Shop not found in this order",
                    StatusCodes.Status404NotFound);

            // check OrderShop status
            if (orderShop.Status != OrderShopStatusEnum.WaitingForRefund)
                throw new AppException(ResponseCodeConstants.BAD_REQUEST,
                    "This shop's order is not in 'Waiting for Refund' status",
                    StatusCodes.Status400BadRequest);

            // create QR for customer refund
            string customerQrUrl = GetCustomerRefundQRUrl(order.Customer, orderShop);

            // create QR for shop payment
            string shopQrUrl = string.Empty;
            if (orderShop.Shop.BankId.HasValue && !string.IsNullOrEmpty(orderShop.Shop.AccountNo))
            {
                decimal shopPayment = CalculateShopPayment(orderShop);
                shopQrUrl = GetShopQRUrl(orderShop, shopPayment);
            }

            await UpdateOrderShopStatusForRefund(order, orderShop, request.UserId);

            return (
                customerRefundQR: customerQrUrl,
                shopPaymentQR: shopQrUrl
            );
        }

        private string GetCustomerRefundQRUrl(UserEntity customer, OrderShop orderShop)
        {
            if (!customer.BankId.HasValue || string.IsNullOrEmpty(customer.AccountNo))
                throw new AppException(ResponseCodeConstants.BAD_REQUEST,
                    "Customer bank information is missing",
                    StatusCodes.Status400BadRequest);

            if (orderShop.TotalDeposit <= 0)
                throw new AppException(ResponseCodeConstants.BAD_REQUEST,
                    "No deposit amount to refund",
                    StatusCodes.Status400BadRequest);

            var parameters = new List<string>
        {
            $"amount={orderShop.TotalDeposit}",
            $"addInfo={Uri.EscapeDataString($"Refund order {orderShop.OrderId} - shop {orderShop.ShopId}")}"
        };

            return $"https://img.vietqr.io/image/{customer.BankId}-{customer.AccountNo}-compact.png?{string.Join("&", parameters)}";
        }

        private string GetShopQRUrl(OrderShop orderShop, decimal amount)
        {
            var parameters = new List<string>
        {
            $"amount={amount}",
            $"addInfo={Uri.EscapeDataString($"Thanh toan don hang {orderShop.Id}")}"
        };

            return $"https://img.vietqr.io/image/{orderShop.Shop.BankId}-{orderShop.Shop.AccountNo}-compact.png?{string.Join("&", parameters)}";
        }

        private decimal CalculateShopPayment(OrderShop orderShop)
        {
            decimal totalPayment = orderShop.TotalRentPrice - orderShop.Commission - orderShop.Tax;

            if (orderShop.Status == OrderShopStatusEnum.ReturnedWithDamage)
            {
                totalPayment += orderShop.TotalDeposit;
            }
            return totalPayment;
        }

        private async Task UpdateOrderShopStatusForRefund(Order order, OrderShop orderShop, int adminId)
        {
            // Update OrderShop status
            orderShop.OrderTrackings.Add(new OrderTracking
            {
                OrderShopId = orderShop.Id,
                Status = OrderShopStatusEnum.Completed.ToString()
            });

            orderShop.Status = OrderShopStatusEnum.Completed;
            orderShop.LastUpdatedBy = adminId;
            orderShop.LastUpdatedTime = CoreHelper.SystemTimeNow;
            orderShop.TotalCustomerRefund = orderShop.TotalDeposit;
            orderShop.TotalShopReceive = CalculateShopPayment(orderShop);

            _orderShopRepository.Update(orderShop);

            bool hasRemainingRefunds = order.OrderShops
                .Any(os => os.Id != orderShop.Id &&
                          os.Status == OrderShopStatusEnum.WaitingForRefund);

            if (!hasRemainingRefunds)
            {
                order.LastUpdatedBy = adminId;
                order.LastUpdatedTime = CoreHelper.SystemTimeNow;
                _orderRepository.Update(order);
            }

            await _unitOfWork.SaveChangeAsync();
        }
    }
}

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
            var orderShop = await _orderShopRepository.GetAllWithCondition()
                .Include(x => x.Order)
                    .ThenInclude(x => x.Customer)
                .Include(x => x.Shop)
                .Include(x => x.OrderTrackings)
                .FirstOrDefaultAsync(os => os.Id == request.OrderShopId);

            if (orderShop == null)
                throw new AppException(ResponseCodeConstants.NOT_FOUND,
                    "OrderShop not found",
                    StatusCodes.Status404NotFound);

            var admin = await _userRepository.GetSingleAsync(u => u.Id == request.UserId);
            if (admin == null)
                throw new AppException(ResponseCodeConstants.NOT_FOUND,
                    "Admin not found",
                    StatusCodes.Status404NotFound);

            if (orderShop.Status != OrderShopStatusEnum.WaitingForRefund)
                throw new AppException(ResponseCodeConstants.BAD_REQUEST,
                    "This order is not in 'Waiting for Refund' status",
                    StatusCodes.Status400BadRequest);

            string customerQrUrl = GetCustomerRefundQR(orderShop);

            string shopQrUrl = string.Empty;
            if (orderShop.Shop.BankId.HasValue && !string.IsNullOrEmpty(orderShop.Shop.AccountNo))
            {
                decimal shopPayment = CalculateShopPayment(orderShop);
                shopQrUrl = GetShopQR(orderShop, shopPayment);
            }

            await UpdateOrderShopStatusForRefund(orderShop, request.UserId);

            return (
                customerRefundQR: customerQrUrl,
                shopPaymentQR: shopQrUrl
            );
        }

        private string GetCustomerRefundQR(OrderShop orderShop)
        {
            var customer = orderShop.Order.Customer;
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
        $"addInfo={Uri.EscapeDataString($"Refund OrderShop {orderShop.Id}")}"
    };

            return $"https://img.vietqr.io/image/{customer.BankId}-{customer.AccountNo}-compact.png?{string.Join("&", parameters)}";
        }

        private string GetShopQR(OrderShop orderShop, decimal amount)
        {
            var parameters = new List<string>
    {
        $"amount={amount}",
        $"addInfo={Uri.EscapeDataString($"Thanh toan OrderShop {orderShop.Id}")}"
    };

            return $"https://img.vietqr.io/image/{orderShop.Shop.BankId}-{orderShop.Shop.AccountNo}-compact.png?{string.Join("&", parameters)}";
        }

        private decimal CalculateShopPayment(OrderShop orderShop)
        {
            // TotalRentPrice - Commission - Tax
            decimal totalPayment = orderShop.TotalRentPrice - orderShop.Commission - orderShop.Tax;

            // Nếu có damage, shop được giữ lại tiền deposit
            if (orderShop.Status == OrderShopStatusEnum.ReturnedWithDamage)
            {
                totalPayment += orderShop.TotalDeposit;
            }
            return totalPayment;
        }

        private async Task UpdateOrderShopStatusForRefund(OrderShop orderShop, int adminId)
        {
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

            bool hasRemainingRefunds = await _orderShopRepository
                .GetAllWithCondition()
                .AnyAsync(os => os.OrderId == orderShop.OrderId &&
                               os.Id != orderShop.Id &&
                               os.Status == OrderShopStatusEnum.WaitingForRefund);

            if (!hasRemainingRefunds)
            {
                orderShop.Order.LastUpdatedBy = adminId;
                orderShop.Order.LastUpdatedTime = CoreHelper.SystemTimeNow;
                _orderRepository.Update(orderShop.Order);
            }

            await _unitOfWork.SaveChangeAsync();
        }
    }
}

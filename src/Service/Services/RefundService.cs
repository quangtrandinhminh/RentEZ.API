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
        private readonly PayOSSetting _payOSSetting;

        public RefundService(
            IBaseRepository<Order> orderRepository,
            IBaseRepository<OrderShop> orderShopRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            PayOSSetting payOSSetting)
        {
            _orderRepository = orderRepository;
            _orderShopRepository = orderShopRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _payOSSetting = payOSSetting;
        }

        public async Task<(string customerRefundQR, string shopPaymentQR)> ProcessRefundAsync(RefundRequest request)
        {
            var order = await _orderRepository.GetAllWithCondition()
                .Include(x => x.Customer)
                .Include(x => x.OrderShops)
                .ThenInclude(x => x.Shop)
                .FirstOrDefaultAsync(o => o.Id == request.OrderId);

            if (order == null)
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsOrder.NOT_FOUND, StatusCodes.Status404NotFound);

            var admin = await _userRepository.GetSingleAsync(u => u.Id == request.UserId);

            if (admin == null)
                throw new AppException(ResponseCodeConstants.NOT_FOUND, "Admin not found", StatusCodes.Status404NotFound);

            var orderShopsToRefund = order.OrderShops.Where(os => os.Status == OrderShopStatusEnum.WaitingForRefund);

            if (!orderShopsToRefund.Any())
                throw new AppException(ResponseCodeConstants.BAD_REQUEST, "There are no order shops in the 'Waiting for Refund' status", StatusCodes.Status400BadRequest);

            if (order.PaymentMethod != PaymentMethodEnum.PayOS)
            {
                throw new AppException(ResponseCodeConstants.BAD_REQUEST, "Unsupported payment method", StatusCodes.Status400BadRequest);
            }

            string customerRefundQR = await GenerateCustomerRefundUrl(order);
            string shopPaymentQR = await GenerateShopPaymentUrl(order);

            await UpdateOrderStatusForRefund(order, request.UserId);
            return (customerRefundQR: customerRefundQR, shopPaymentQR: shopPaymentQR);
        }

        private async Task<string> GenerateCustomerRefundUrl(Order order)
        {
            if (!order.Customer.BankId.HasValue || string.IsNullOrEmpty(order.Customer.AccountNo))
                throw new AppException(ResponseCodeConstants.BAD_REQUEST, "Customer bank information is missing", StatusCodes.Status400BadRequest);

            if (order.TotalDeposit <= 0)
                throw new AppException(ResponseCodeConstants.BAD_REQUEST, "No deposit amount to refund", StatusCodes.Status400BadRequest);

            var qrRequest = new VietQRRequest
            {
                Amount = order.TotalDeposit,
                AccountNo = order.Customer.AccountNo,
                AccountName = order.Customer.AccountName ?? string.Empty,
                BankId = order.Customer.BankId.Value,
                Description = $"Refund order with id {order.Id}"
            };

            return await GeneratePayOSPaymentUrl(qrRequest);
        }

        private async Task<string> GenerateShopPaymentUrl(Order order)
        {
            var paymentUrls = new List<string>();

            foreach (var orderShop in order.OrderShops)
            {
                if (!orderShop.Shop.BankId.HasValue || string.IsNullOrEmpty(orderShop.Shop.AccountNo))
                    continue;

                var shopPayment = CalculateShopPayment(orderShop);

                var qrRequest = new VietQRRequest
                {
                    Amount = shopPayment,
                    AccountNo = orderShop.Shop.AccountNo,
                    AccountName = orderShop.Shop.AccountName ?? string.Empty,
                    BankId = orderShop.Shop.BankId.Value,
                    Description = $"Thanh toan tien thue don hang {orderShop.Id}"
                };

                var paymentUrl = await GeneratePayOSPaymentUrl(qrRequest);
                paymentUrls.Add(paymentUrl);
            }

            return string.Join(Environment.NewLine, paymentUrls);
        }

        private async Task<string> GeneratePayOSPaymentUrl(VietQRRequest request)
        {
            try
            {
                var payOS = new PayOS(_payOSSetting.ClientID, _payOSSetting.ApiKey, _payOSSetting.ChecksumKey);
                string truncatedDescription = request.Description.Length > 25 ? request.Description.Substring(0, 25) : request.Description;
                var itemData = new ItemData(
                    truncatedDescription,
                    1,
                    (int)(request.Amount)
                );

                var domain = SystemSettingModel.Instance.Domain ?? "http://localhost";
                var dummyCancelUrl = $"{domain}/dummy-cancel";
                var dummyReturnUrl = $"{domain}/dummy-return";

                var paymentData = new PaymentData(
                    orderCode: long.Parse(CoreHelper.SystemTimeNow.ToString("ffffff")),
                    amount: (int)(request.Amount),
                    description: truncatedDescription,
                    items: new List<ItemData> { itemData },
                    cancelUrl: dummyCancelUrl,
                    returnUrl: dummyReturnUrl,
                    signature: "",
                    buyerName: "",
                    buyerEmail: "",
                    buyerPhone: "",
                    buyerAddress: "",
                    expiredAt: (int)CoreHelper.SystemTimeNow.AddMinutes(15).ToUnixTimeSeconds()
                );
                var result = await payOS.createPaymentLink(paymentData);
                return result.checkoutUrl;
            }
            catch (Exception)
            {
                throw new AppException(
                    ResponseCodeConstants.INTERNAL_SERVER_ERROR,
                    "Failed to generate payment URL",
                    StatusCodes.Status500InternalServerError);
            }
        }

        private decimal CalculateShopPayment(OrderShop orderShop)
        {
            var order = _orderRepository.GetAllWithCondition().Include(x => x.OrderShops).FirstOrDefault(o => o.Id == orderShop.OrderId);

            if (order == null)
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsOrder.NOT_FOUND, StatusCodes.Status404NotFound);

            decimal totalPayment = order.OrderShops.Sum(shop =>
                shop.TotalRentPrice - shop.Commission - shop.Tax);

            if (orderShop.Status == OrderShopStatusEnum.ReturnedWithDamage)
            {
                totalPayment += orderShop.TotalDeposit;
            }
            return totalPayment;
        }

        private async Task UpdateOrderStatusForRefund(Order order, int adminId)
        {
            foreach (var orderShop in order.OrderShops)
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
            }
            order.LastUpdatedBy = adminId;
            order.LastUpdatedTime = CoreHelper.SystemTimeNow;
            _orderRepository.Update(order);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}

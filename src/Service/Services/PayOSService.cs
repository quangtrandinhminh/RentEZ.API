using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Net.payOS;
using Net.payOS.Types;
using Newtonsoft.Json;
using Repository.Base;
using Repository.Infrastructure;
using Repository.Models;
using Serilog;
using Service.ApiModels.PayOS;
using Service.BusinessModels;
using Service.Interfaces;
using Utility.Config;
using Utility.Constants;
using Utility.Enum;
using Utility.Exceptions;
using Utility.Helpers;

namespace Service.Services;

public class PayOSService(IServiceProvider serviceProvider) : IPayOSService
{
    private readonly IUnitOfWork _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
    private readonly IBaseRepository<Order> _orderRepository = serviceProvider.GetRequiredService<IBaseRepository<Order>>();
    private readonly IBaseRepository<OrderShop> _orderShopRepository = serviceProvider.GetRequiredService<IBaseRepository<OrderShop>>();
    private readonly ILogger _logger = serviceProvider.GetRequiredService<ILogger>();
    private readonly PayOSSetting _payOSSetting = PayOSSetting.Instance;
    private const string PAYMENT_DESCRIPTION = "RentEZ don hang";

    public async Task<CreatePaymentResult> CreatePaymentLinkAsync(PayOSRequest request)
    {
        _logger.Information($"Create payment link for order {request.OrderId}");
        var order = await _orderRepository.GetSingleAsync(o => o.Id == request.OrderId, 
            false, o => o.Customer);
        if (order == null)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsOrder.NOT_FOUND, StatusCodes.Status404NotFound);
        }

        var payOS = new PayOS(_payOSSetting.ClientID, _payOSSetting.ApiKey, _payOSSetting.ChecksumKey);
        // Create an item with the order ID and customer name
        ItemData item = new ItemData($"{PAYMENT_DESCRIPTION} {order.Id} cho khach hang {order.Customer.FullName}",
            1, (int) order.Total);
        List<ItemData> items = new List<ItemData>();
        items.Add(item);

        // ConvertDateTimeToUnixTimestamp
        var expiredAt = ConvertDateTimeToUnixTimestamp(CoreHelper.SystemTimeNow.AddMinutes(15));
        int orderCode = int.Parse(CoreHelper.SystemTimeNow.ToString("ffffff"));

        // Create a PaymentData object
        PaymentData paymentData = new PaymentData(orderCode, (int) order.Total, $"{PAYMENT_DESCRIPTION} {order.Id}",
            items, request.CancelUrl, request.ReturnUrl, expiredAt: expiredAt);

        // Create a signature for the payment data
        var signature = CreateSignature(payOS, paymentData);
        paymentData = paymentData with { signature = signature };

        CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);

        // update order
        order.PaymentId = orderCode.ToString();
        order.PaymentMethod = PaymentMethodEnum.PayOS;
        order.PaymentStatus = PaymentStatusEnum.Pending;
        _orderRepository.Update(order);
        await _unitOfWork.SaveChangeAsync();
        return createPayment;
    }

    private string CreateSignature(PayOS payOs, PaymentData paymentData)
    {
        // Prepare data by extracting properties from the PaymentData object, sorted by property name
        var properties = paymentData.GetType().GetProperties()
            .OrderBy(p => p.Name)
            // Ensure value is not null
            .Where(p => p.GetValue(paymentData) != null);  

        StringBuilder sb = new StringBuilder();

        // Build the query string (key1=value1&key2=value2...)
        foreach (var property in properties)
        {
            var value = property.GetValue(paymentData)?.ToString();
            // Skip if the value is an empty string
            if (!string.IsNullOrEmpty(value))  
            {
                sb.Append($"{property.Name}={value}&");
            }
        }

        // Remove the last '&' if it exists
        if (sb.Length > 0)
        {
            // Efficient way to remove the trailing '&'
            sb.Length--; 
        }

        // Encrypt the resulting string using HMAC-SHA256
        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_payOSSetting.ChecksumKey)))
        {
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
            // Convert hash to lowercase hexadecimal
            return BitConverter.ToString(hash).Replace("-", "").ToLower();  
        }
    }

    // convert datetimeoffset to unix timestamp
    private int ConvertDateTimeToUnixTimestamp(DateTimeOffset dateTime)
    {
        return (int)dateTime.ToUnixTimeSeconds();
    }

    // Wait for WebhookType response from payos server 
    public async Task GetWebhookTypeAsync(WebhookType request)
    {
        _logger.Information("Get webhook type {@request}", request.data);
        var orderCode = request.data.orderCode;
        _logger.Information($"Get webhook signature {orderCode}");
        var payOs = new PayOS(_payOSSetting.ClientID, _payOSSetting.ApiKey, _payOSSetting.ChecksumKey);
        WebhookData data = payOs.verifyPaymentWebhookData(request);
        if (data == null)
        {
            _logger.Information("Webhook data not found");
            return;
        }

        // orderId is the last part of the description
        var orderId = int.Parse(request.data.description.Split(" ").Last());
        _logger.Information($"Get order {orderId} with orderCode {orderCode}");

        // valid data & change status of order
        var order = await GetOrderById(orderId);
        if (order == null)
        {
            _logger.Information($"Order {orderId} not found");
            return;
        }

        // if status == "00" change status of order to success
        if (request.code == "00")
        {
            UpdateOrderPayment(order, data.orderCode);
        }
        else
        {
            _logger.Information($"Request failed for order {orderId}");
        }
    }

    public async Task<PaymentLinkInformation> GetPaymentLinkInformationAsync(long orderCode)
    {
        _logger.Information($"Get payment link information for orderCode {orderCode}");
        /*var payOS = new PayOS(_payOSSetting.ClientID, _payOSSetting.ApiKey, _payOSSetting.ChecksumKey);
        return await payOS.getPaymentLinkInformation(orderCode);*/

        // fetch api from payOS https://api-merchant.payos.vn/v2/payment-requests/{orderCode} with header x-client-id, x-api-key

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("x-client-id", _payOSSetting.ClientID);
        client.DefaultRequestHeaders.Add("x-api-key", _payOSSetting.ApiKey);
        var response = await client.GetAsync($"https://api-merchant.payos.vn/v2/payment-requests/{orderCode}");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var payOsResponse = JsonConvert.DeserializeObject<PayOSResponse>(content);
            return payOsResponse.Data;
        }

        throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsOrder.NOT_FOUND, StatusCodes.Status404NotFound);
    }

    // localhost only to confirm payment
    public async Task ConfirmPayment(int orderId, long orderCode)
    {
        _logger.Information($"Confirm payment for order {orderId} with orderCode {orderCode}");
        var order = await GetOrderById(orderId);
        if (order.PaymentStatus == PaymentStatusEnum.Paid)
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, 
                               ResponseMessageConstrantsOrder.ALREADY_PAID, StatusCodes.Status400BadRequest);
        }

        var payment = await GetPaymentLinkInformationAsync(orderCode);
        if (payment == null)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, 
                                              ResponseMessageConstrantsOrder.NOT_FOUND, StatusCodes.Status404NotFound);
        }

        if (payment.status != "PAID")
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, 
                                              ResponseMessageConstrantsOrder.NOT_PAID, StatusCodes.Status400BadRequest);
        }

        UpdateOrderPayment(order, orderCode);
    }

    private void UpdateOrderPayment(Order order, long orderCode)
    {
        _logger.Information($"Update order payment for order {order.Id} with orderCode {orderCode}");

        // update order
        order.PaymentStatus = PaymentStatusEnum.Paid;
        order.PaymentTime = CoreHelper.SystemTimeNow;
        order.PaymentMethod = PaymentMethodEnum.PayOS;
        order.PaymentId = orderCode.ToString();
        _orderRepository.Update(order);

        // update order shop
        foreach (var orderShop in order.OrderShops)
        {
            OrderShopProcessor processor = new OrderShopProcessor(orderShop);
            processor.ChangeNextStatus();
            _orderShopRepository.Update(orderShop);
        }

        _unitOfWork.SaveChange();
    }

    private async Task<Order> GetOrderById(int orderId)
    {
        _logger.Information($"Get order by id {orderId}");
        var order = await _orderRepository.GetSingleAsync(o => o.Id == orderId, 
                       false, o => o.OrderShops);
        if (order == null)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, 
                               ResponseMessageConstrantsOrder.NOT_FOUND, StatusCodes.Status404NotFound);
        }
        return order;
    }

    public async Task<string> ConfirmWebhook (string webhookUrl)
    {
        var payOs = new PayOS(_payOSSetting.ClientID, _payOSSetting.ApiKey, _payOSSetting.ChecksumKey);
        return await payOs.confirmWebhook(webhookUrl);
    }
}
using Microsoft.AspNetCore.Http;
using Service.ApiModels.VNPay;

namespace Service.Interfaces;

public interface IVnPayService
{
    Task<string> CreatePaymentUrl(HttpContext context, VnPaymentRequest request);
    Task<VnPaymentResponse> PaymentExecute(HttpContext context, int orderId);
}
using Net.payOS.Types;
using Service.ApiModels.PayOS;

namespace Service.Interfaces;

public interface IPayOSService
{
    Task<CreatePaymentResult> CreatePaymentLinkAsync(PayOSRequest request);
    Task GetWebhookTypeAsync(WebhookType request);
    Task<PaymentLinkInformation> GetPaymentLinkInformationAsync(int orderId);
    Task ConfirmPayment(int orderId, string paymentId);
}
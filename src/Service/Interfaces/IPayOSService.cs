using Net.payOS.Types;
using Service.ApiModels.PayOS;

namespace Service.Interfaces;

public interface IPayOSService
{
    Task<CreatePaymentResult> CreatePaymentLinkAsync(PayOSRequest request);
    Task GetWebhookTypeAsync(WebhookType request);
    Task<PaymentLinkInformation> GetPaymentLinkInformationAsync(long orderCode);
    Task ConfirmPayment(int orderId, long orderCode);
    Task<string> ConfirmWebhook(string webhookUrl);
}
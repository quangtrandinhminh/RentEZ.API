using Service.ApiModels.Refund;

namespace Service.Interfaces
{
    public interface IRefundService
    {
        Task<(string customerRefundQR, string shopPaymentQR)> ProcessRefundAsync(RefundRequest request);
    }
}

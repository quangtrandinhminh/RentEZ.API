using Service.ApiModels.Voucher;

namespace Service.ApiModels.Order;

public class CartCalcRequest
{
    public IList<VoucherCalcRequest?> Vouchers { get; set; } = new List<VoucherCalcRequest?>();
    public IList<CartItemCalcRequest> CartItems { get; set; } = new List<CartItemCalcRequest>();
}
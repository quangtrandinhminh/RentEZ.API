using Service.ApiModels.Voucher;

namespace Service.ApiModels.Order;

public class CartShopCalcRequest
{
    public int ShopId { get; set; }
    public IList<VoucherCalcRequest>? ShopVouchers { get; set; }
    public IList<CartItemCalcRequest> CartItems { get; set; } = new List<CartItemCalcRequest>();
}
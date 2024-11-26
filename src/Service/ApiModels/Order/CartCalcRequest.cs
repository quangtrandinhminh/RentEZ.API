namespace Service.ApiModels.Order;

public class CartCalcRequest
{
    public IList<int>? VoucherIds { get; set; }
    public IList<CartItemCalcRequest> CartItems { get; set; } = new List<CartItemCalcRequest>();
}
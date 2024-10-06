namespace Service.ApiModels.Order;

public class OrderCalcRequest
{
    public int? VoucherId { get; set; }
    public IList<OrderCalcRequest> OrderDetails { get; set; } = new List<OrderCalcRequest>();
}
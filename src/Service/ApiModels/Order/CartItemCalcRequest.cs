namespace Service.ApiModels.Order;

public class CartItemCalcRequest
{
    public int ProductId { get; set; }
    public int RentQuantity { get; set; }
    public DateTimeOffset RentDateTime { get; set; }
    public DateTimeOffset ReturnDateTime { get; set; }
}
namespace Service.ApiModels.Order;

public class OrderDetailListResponse
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotalRentPrice { get; set; }
    public decimal SubTotalDeposit { get; set; }
    public decimal Discount { get; set; }
    public decimal SubTotal { get; set; }
    public DateTimeOffset RentDateTime { get; set; }
    public DateTimeOffset ReturnDateTime { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
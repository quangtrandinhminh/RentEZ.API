namespace Service.ApiModels.OrderDetail;

public class OrderDetailListResponse
{
    public int Id { get; set; }
    public int OrderShopId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; } 
    public decimal SubTotalRentPrice { get; set; }
    public decimal SubTotalDeposit { get; set; }
}
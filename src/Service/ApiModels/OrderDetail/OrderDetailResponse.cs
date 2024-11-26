namespace Service.ApiModels.OrderDetail;

public class OrderDetailResponse : OrderDetailListResponse
{ 
    public int ShopId { get; set; }
    public int RentQuantity { get; set; }
    public DateTimeOffset RentDateTime { get; set; }
    public DateTimeOffset ReturnDateTime { get; set; }
    public decimal Price { get; set; }
    public decimal RentPrice { get; set; }
    public decimal Deposit { get; set; }
    public decimal DepositRate { get; set; }
    public decimal Discount { get; set; }
    public decimal SubTotal { get; set; }
}
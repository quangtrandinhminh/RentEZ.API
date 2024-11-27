namespace Service.ApiModels.OrderDetail;

public class OrderDetailListResponse
{
    public int Id { get; set; }
    public int OrderShopId { get; set; }
    public int ProductId { get; set; }
    public OdProductResponse Product { get; set; }
    public decimal SubTotalRentPrice { get; set; }
    public decimal SubTotalDeposit { get; set; }
    public int RentQuantity { get; set; }
    public DateTimeOffset RentDateTime { get; set; }
    public DateTimeOffset ReturnDateTime { get; set; }
    public DateTimeOffset ReceiveTimeOnSchedule { get; set; }
    public DateTimeOffset ReturnTimeOnSchedule { get; set; }
    public decimal Price { get; set; }
    public decimal RentPrice { get; set; }
    public decimal Deposit { get; set; }
    public decimal DepositRate { get; set; }
    public bool IsRated { get; set; }
}
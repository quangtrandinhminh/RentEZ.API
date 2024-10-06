namespace Service.ApiModels.Order;

public class OrderDetailCalcRequest
{
    public int ProductId { get; set; }
    public int? VoucherId { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset RentDateTime { get; set; }
    public DateTimeOffset ReturnDateTime { get; set; }
    public decimal Price { get; set; }
    public decimal RentPrice { get; set; }
    public decimal Deposit { get; set; }
    public decimal DepositRate { get; set; }
}
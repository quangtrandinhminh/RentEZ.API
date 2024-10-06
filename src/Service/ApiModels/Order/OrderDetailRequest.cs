namespace Service.ApiModels.Order;

public class OrderDetailRequest : OrderDetailCalcRequest
{
    public decimal SubTotalRentPrice { get; set; }
    public decimal SubTotalDeposit { get; set; }
    public decimal Discount { get; set; }
    public decimal SubTotal { get; set; }
}
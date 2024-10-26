using Service.ApiModels.Order;

namespace Service.ApiModels.OrderDetail;

public class OrderDetailRequest : CartItemCalcRequest
{
    public int ShopId { get; set; }
    public decimal Price { get; set; }
    public decimal RentPrice { get; set; }
    public decimal Deposit { get; set; }
    public decimal DepositRate { get; set; }
    public decimal SubTotalRentPrice { get; set; }
    public decimal SubTotalDeposit { get; set; }
    public decimal Discount { get; set; }
    public decimal SubTotal { get; set; }
}
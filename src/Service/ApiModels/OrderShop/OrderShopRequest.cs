using Service.ApiModels.OrderDetail;

namespace Service.ApiModels.OrderShop;

public class OrderShopRequest
{
    public int ShopId { get; set; }
    public string? ShopName { get; set; }
    public int? VoucherId { get; set; }
    public decimal TaxRate { get; set; }
    public decimal Tax { get; set; }
    public decimal CommissionRate { get; set; }
    public decimal Commission { get; set; }
    public decimal ShipFee { get; set; }
    public decimal ShipFeeSupport { get; set; } = 0;
    public decimal PlatformRentPriceDiscount { get; set; }
    public decimal ShopRentPriceDiscount { get; set; }
    public decimal TotalRentPrice { get; set; }
    public decimal TotalDeposit { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal Total { get; set; }
    public IList<OrderDetailRequest> OrderDetails { get; set; } = new List<OrderDetailRequest>();
}
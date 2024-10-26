using Service.ApiModels.OrderShop;
using Service.ApiModels.Voucher;
using Utility.Enum;

namespace Service.ApiModels.Order;

public class OrderRequest
{
    public int CustomerId { get; set; }
    public string OrderAddress { get; set; }
    public decimal TotalRentPrice { get; set; }
    public decimal TotalDeposit { get; set; }
    public decimal TotalShipFee { get; set; }
    public decimal ShipSupportFee { get; set; }
    public decimal CommissionRate { get; set; }
    public decimal TotalCommissionFee { get; set; }
    public decimal Tax { get; set; }
    public decimal TaxRate { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal Total { get; set; }
    public string? Note { get; set; }
    public PaymentMethodEnum? PaymentMethod { get; set; }
    public IList<VoucherRequest?> PlatformVouchers { get; set; }
    public IList<OrderShopRequest> OrderShops { get; set; }
    public int ShopCount => OrderShops.Count;
    public int ItemCount => OrderShops.Sum(x => x.OrderDetails.Sum(y => y.RentQuantity));
}
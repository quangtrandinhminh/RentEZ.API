using Service.ApiModels.OrderDetail;
using Service.ApiModels.OrderShop;
using Service.ApiModels.Voucher;

namespace Service.ApiModels.Order;

public class OrderResponse : OrderListResponse
{
    public decimal ShipFee { get; set; }
    public decimal ShipSupportFee { get; set; }
    public decimal Tax { get; set; }
    public decimal TaxRate { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal Total { get; set; }
    public string? PaymentId { get; set; }
    public string? Note { get; set; }
    public IList<VoucherResponse> PlatformVouchers { get; set; } = new List<VoucherResponse>();
    public IList<OrderShopListResponse> OrderShops { get; set; } = new List<OrderShopListResponse>();
}
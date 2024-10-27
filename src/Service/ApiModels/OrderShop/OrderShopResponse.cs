using Repository.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Service.ApiModels.OrderDetail;
using Service.ApiModels.Voucher;
using Utility.Enum;

namespace Service.ApiModels.OrderShop;

public class OrderShopResponse : OrderShopListResponse
{
    public int? VoucherId { get; set; }
    public decimal TaxRate { get; set; }
    public decimal Tax { get; set; }
    public decimal CommissionRate { get; set; }
    public decimal Commission { get; set; }
    public decimal ShipFee { get; set; }
    public decimal ShipFeeSupport { get; set; }
    public decimal PlatformRentPriceDiscount { get; set; }
    public decimal ShopRentPriceDiscount { get; set; }
    public decimal TotalDiscount { get; set; }
    public virtual VoucherResponse? Voucher { get; set; }
    public virtual IList<OrderTracking> OrderTrackings { get; set; } = new List<OrderTracking>();
}
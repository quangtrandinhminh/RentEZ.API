using System.ComponentModel.DataAnnotations.Schema;
using Repository.Models.Base;
using Utility.Enum;

namespace Repository.Models;

public class OrderShop : BaseEntity
{
    public OrderShop()
    {
        Status = OrderShopStatusEnum.WaitForPayment;
        var orderTracking = new OrderTracking();
        orderTracking.Status = OrderShopStatusEnum.WaitForPayment.ToString();
        OrderTrackings.Add(orderTracking);
    }
    public int ShopId { get; set; }
    public int OrderId { get; set; }
    public int? VoucherId { get; set; }
    public decimal TaxRate { get; set; }
    public decimal Tax { get; set; }
    public decimal CommissionRate { get; set; }
    public decimal Commission { get; set; }
    public decimal ShipFee { get; set; }
    public decimal ShipFeeSupport { get; set; }
    public decimal TotalRentPrice { get; set; }
    public decimal PlatformRentPriceDiscount { get; set; }
    public decimal ShopRentPriceDiscount { get; set; }
    public decimal TotalDeposit { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal Total { get; set; }
    public OrderShopStatusEnum Status { get; set; }
    public decimal? TotalCustomerRefund { get; set; }
    public DateTimeOffset? CustomerReceiveTime { get; set; }
    public decimal? TotalShopReceive { get; set; }
    public DateTimeOffset? ShopReceiveTime { get; set; }
    public int? CancelBy { get; set; }
    public string? CancelReason { get; set; }
    public string? Note { get; set; }

    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; }

    [ForeignKey("ShopId")]
    public virtual Shop Shop { get; set; }

    [ForeignKey("VoucherId")]
    public virtual Voucher? Voucher { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public virtual ICollection<OrderTracking> OrderTrackings { get; set; } = new List<OrderTracking>();
}
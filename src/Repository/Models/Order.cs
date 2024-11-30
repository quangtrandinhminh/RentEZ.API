using System.ComponentModel.DataAnnotations.Schema;
using Repository.Models.Base;
using Repository.Models.Identity;
using Utility.Enum;

namespace Repository.Models;

public class Order : BaseEntity
{
    public Order()
    {
        PaymentStatus = PaymentStatusEnum.Pending;
    }
    public int CustomerId { get; set; }
    public string? OrderAddress { get; set; }
    public decimal TotalRentPrice { get; set; } 
    public decimal TotalDeposit { get; set; } 
    public decimal ShipFee { get; set; } = 0;
    public decimal ShipSupportFee { get; set; } = 0;
    public decimal ShopRentDiscount { get; set; } 
    public decimal PlatformRentDiscount { get; set; } 
    public decimal Tax { get; set; } 
    public decimal TaxRate { get; set; } 
    public decimal TotalDiscount { get; set; } 
    public decimal ProvisionalTotal { get; set; } 
    public decimal Total { get; set; } 
    public PaymentStatusEnum PaymentStatus { get; set; }
    public PaymentMethodEnum PaymentMethod { get; set; }
    public DateTimeOffset? PaymentTime { get; set; }
    public string? PaymentId { get; set; }
    public string? Note { get; set; }

    [ForeignKey("CustomerId")]
    public virtual UserEntity Customer { get; set; }
    public virtual ICollection<Voucher> PlatformVouchers { get; set; } = new List<Voucher>();
    public virtual ICollection<OrderShop> OrderShops { get; set; } = new List<OrderShop>();
}
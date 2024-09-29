using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Models.Base;
using Repository.Models.Identity;
using Utility.Enum;
using Utility.Helpers;

namespace Repository.Models;

public class Order : BaseEntity
{
    public int CustomerId { get; set; }
    public int? VoucherId { get; set; }
    public string OrderAddress { get; set; }
    public decimal TotalRentPrice { get; set; }
    public decimal TotalDeposit { get; set; }
    public decimal ShipFee { get; set; }
    public decimal PlatformRate { get; set; }
    public decimal PlatformFee { get; set; }
    public decimal ShipSupportFee { get; set; }
    public decimal Tax { get; set; }
    public decimal TaxRate { get; set; }
    public decimal Total { get; set; }
    public PaymentStatusEnum PaymentStatus { get; set; }
    public PaymentMethodEnum PaymentMethod { get; set; }
    public string Note { get; set; }

    [ForeignKey("CustomerId")]
    public virtual UserEntity Customer { get; set; }

    [ForeignKey("VoucherId")]
    public virtual Voucher Voucher { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}
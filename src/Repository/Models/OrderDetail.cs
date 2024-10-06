using System.ComponentModel.DataAnnotations.Schema;
using Repository.Models.Base;

namespace Repository.Models;

public class OrderDetail : BaseEntity
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int? VoucherId { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset RentDateTime { get; set; }
    public DateTimeOffset ReturnDateTime { get; set; }
    public DateTimeOffset ReceiveTimeOnSchedule => RentDateTime.AddDays(-1);
    public DateTimeOffset ReturnTimeOnSchedule => ReturnDateTime.AddDays(1);
    public decimal Price { get; set; }
    public decimal RentPrice { get; set; }
    public decimal Deposit { get; set; }
    public decimal DepositRate { get; set; }
    public decimal SubTotalRentPrice { get; set; }
    public decimal SubTotalDeposit { get; set; }
    public decimal? Discount { get; set; }
    public decimal SubTotal { get; set; }
    public byte? Rating { get; set; }
    public string? ReviewContent { get; set; }

    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    [ForeignKey("VoucherId")]
    public virtual Voucher? Voucher { get; set; }
}
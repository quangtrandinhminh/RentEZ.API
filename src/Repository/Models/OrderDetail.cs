using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models;

public class OrderDetail
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int OrderShopId { get; set; }
    public int RentQuantity { get; set; }
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
    public byte? Rating { get; set; }
    public string? ReviewContent { get; set; }
    public DateTimeOffset? FeedbackCreatedTime { get; set; }
    public DateTimeOffset? FeedbackLastUpdatedTime { get; set; }
    public bool IsRated => FeedbackCreatedTime != null;

    [ForeignKey("OrderShopId")]
    public virtual OrderShop OrderShop { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
}
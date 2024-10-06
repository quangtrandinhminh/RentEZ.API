using Utility.Enum;

namespace Service.ApiModels.Order;

public class OrderListResponse
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public string OrderAddress { get; set; }
    public decimal TotalRentPrice { get; set; }
    public decimal TotalDeposit { get; set; }
    public decimal Total { get; set; }
    public PaymentStatusEnum PaymentStatus { get; set; }
    public PaymentMethodEnum? PaymentMethod { get; set; }
    public string? Note { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
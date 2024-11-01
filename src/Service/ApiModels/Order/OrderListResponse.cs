using Utility.Enum;

namespace Service.ApiModels.Order;

public class OrderListResponse
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string OrderAddress { get; set; }
    public decimal TotalRentPrice { get; set; }
    public decimal TotalDeposit { get; set; }
    public decimal Total { get; set; }
    public string PaymentStatus { get; set; }
    public string PaymentMethod { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
}
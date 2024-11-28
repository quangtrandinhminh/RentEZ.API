namespace Service.ApiModels.Voucher;

public class VoucherResponse
{
    public int Id { get; set; }
    public int? ShopId { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? ValueType { get; set; }
    public string? Description { get; set; }
    public decimal Value { get; set; }
    public decimal? MinRentValue { get; set; }
    public decimal? MaxDiscountValue { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public string? Status { get; set; }
    public int? Limit { get; set; }
    public int Used { get; set; }
    public bool IsUsed { get; set; }
}
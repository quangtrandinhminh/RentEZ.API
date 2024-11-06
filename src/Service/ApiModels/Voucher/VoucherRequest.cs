using System.ComponentModel.DataAnnotations;
using Utility.Enum;

namespace Service.ApiModels.Voucher;

public class VoucherRequest
{
    public decimal Value { get; set; }
    public decimal? MinRentValue { get; set; }
    public decimal? MaxDiscountValue { get; set; }
    public VoucherTypeEnum Type { get; set; }
    public VoucherValueTypeEnum ValueType { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public string? Description { get; set; }
}
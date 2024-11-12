using System.ComponentModel.DataAnnotations;
using Utility.Enum;

namespace Service.ApiModels.Voucher;

public class VoucherRequest
{
    public string? Code { get; set; }
    public string? Name { get; set; }

    [Required(ErrorMessage = "Value is required")]
    public decimal Value { get; set; }
    public decimal? MinRentValue { get; set; }
    public decimal? MaxDiscountValue { get; set; }

    [Required(ErrorMessage = "Type is required")]
    public VoucherTypeEnum Type { get; set; }

    [Required(ErrorMessage = "Value type is required")]
    public VoucherValueTypeEnum ValueType { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public string? Description { get; set; }
}
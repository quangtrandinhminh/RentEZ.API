using System.ComponentModel.DataAnnotations;
using Utility.Enum;

namespace Service.ApiModels.Voucher;

public class VoucherRequest
{
    [Required(ErrorMessage = "Vui lòng nhập mã voucher")]
    [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Mã voucher viết hoa không cách, không chứa ký tự đặc biệt")]
    public string? Code { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên voucher")]
    [StringLength(50, ErrorMessage = "Tên voucher không quá 50 ký tự")]
    // chỉ không cho phép nhập kí tự đặc biệt
    [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Tên voucher không chứa ký tự đặc biệt")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập giá trị giảm giá")]
    public decimal Value { get; set; }
    public decimal? MinRentValue { get; set; }
    public decimal? MaxDiscountValue { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập loại voucher")]
    public VoucherTypeEnum Type { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập loại giảm giá")]
    public VoucherValueTypeEnum ValueType { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public int? Limit { get; set; }
    public string? Description { get; set; }
}
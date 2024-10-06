using Repository.Models.Base;
using Utility.Enum;

namespace Repository.Models;

public class Voucher : BaseEntity
{
    public Voucher()
    {
        Used = 0;
        Status = VoucherStatusEnum.Preparing;
    }

    public string? Code { get; set; }
    public decimal Value { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public VoucherTypeEnum Type { get; set; }
    public VoucherStatusEnum Status { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public int? Limit { get; set; }
    public int Used { get; set; }
    public string? Note { get; set; }
}
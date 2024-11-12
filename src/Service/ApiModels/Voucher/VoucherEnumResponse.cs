using Utility.Enum;

namespace Service.ApiModels.Voucher;

public class VoucherEnumResponse
{
    public IDictionary<int , string> VoucherTypeEnums { get; set; }
    public IDictionary<int, string> VoucherValueTypeEnums { get; set; }
    public IDictionary<int, string> VoucherStatusEnums { get; set; }
}
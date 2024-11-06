using Service.ApiModels.PaginatedList;
using Utility.Enum;

namespace Service.ApiModels.Voucher;

public class VoucherQueryRequest : PaginatedListRequest
{
    public int? Type { get; set; }
    public int? ValueType { get; set; }
    public int? CustomerId { get; set; }
    public int? ShopId { get; set; }
    public VoucherStatusEnum? Status { get; set; }
}
using Service.ApiModels.PaginatedList;

namespace Service.ApiModels.Voucher;

public class VoucherQueryRequest : PaginatedListRequest
{
    public int? Type { get; set; }
    public int? ValueType { get; set; }
    public int? UserId { get; set; }
    public int? ShopId { get; set; }
    public bool? IsUsed { get; set; }
    public bool? IsExpired { get; set; }
    public bool? IsPreparing { get; set; }
    public bool? IsLimited { get; set; }
    public bool? IsActive { get; set; }
}
using Service.ApiModels.PaginatedList;
using Utility.Enum;

namespace Service.ApiModels.OrderShop;

public class OrderShopListRequest : PaginatedListRequest
{
    public bool IsShop { get; set; } = false;
    public OrderShopStatusEnum? Status { get; set; }
}
using Service.ApiModels.PaginatedList;

namespace Service.ApiModels.OrderDetail;

public class FeedbackListRequest : PaginatedListRequest
{
    public int? ShopId { get; set; }
    public int? ProductId { get; set; }
}
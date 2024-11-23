using Service.ApiModels.OrderDetail;
using Utility.Enum;

namespace Service.ApiModels.OrderShop;

public class OrderShopListResponse
{
    public int Id { get; set; }
    public int ShopId { get; set; }
    public OsShopResponse Shop { get; set; }
    public int OrderId { get; set; }
    public decimal TotalDeposit { get; set; }
    public decimal TotalRentPrice { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
    public virtual IList<OrderDetailListResponse> OrderDetails { get; set; } = new List<OrderDetailListResponse>();
}
using Repository.Extensions;
using Repository.Models;
using Service.ApiModels;
using Service.ApiModels.Order;
using Service.ApiModels.OrderDetail;
using Service.ApiModels.OrderShop;
using Service.ApiModels.PaginatedList;
using Utility.Enum;

namespace Service.Interfaces;

public interface IOrderService
{
    IList<EnumResponse> GetAllPaymentEnum();

    // calculate total price of an order
    Task<OrderRequest> CalculateTotalPrice(CartCalcRequest request);

    // get all orders for all roles
    Task<PaginatedListResponse<OrderListResponse>> GetAllOrdersAsync(OrderListRequest request);

    // get order include order details by id
    Task<OrderResponse> GetOrderByIdAsync(int orderId);

    // user create order
    Task<int> CreateOrderAsync(OrderRequest request, CancellationToken cancellationToken = default);
    IDictionary<int, string> GetAllOrderShopStatusEnum();
    IList<EnumResponse> GetAllOrderShopStatusEnumV2();
    // get all order details for shop owner, user, support query
    Task<PaginatedListResponse<OrderShopListResponse>> GetAllOrderShopsAsync(
        OrderShopListRequest request);

    // get order detail by id, include order
    Task<OrderShopResponse> GetOrderShopsByIdAsync(int orderShopId, bool isShop);

    Task CancelOrderAsync(int orderId);
    Task<string> ChangeOrderShopStatusAsync(int orderShopId, bool isShop);
}
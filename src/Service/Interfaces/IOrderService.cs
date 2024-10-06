using Repository.Models;
using Service.ApiModels.Order;

namespace Service.Interfaces;

public interface IOrderService
{
    // calculate total price of an order
    Task<OrderResponse> CalculateTotalPrice(OrderCalcRequest request);

    // get all orders for admin
    Task<IList<OrderListResponse>> GetAllOrdersAsync(int userId);

    // get order include order details by id
    Task<OrderResponse> GetOrderByIdAsync(int orderId);

    // user create order
    Task<Order> CreateOrderAsync(OrderRequest request);

    // get all order details for shop owner, user, support query
    Task<IList<OrderDetailListResponse>> GetAllOrderDetailsAsync(int userId);

    // get order detail by id, include order
    Task<OrderDetailResponse> GetOrderDetailByIdAsync(int userId, int orderDetailId);
}
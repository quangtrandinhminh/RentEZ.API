using Microsoft.Extensions.DependencyInjection;
using Repository.Base;
using Repository.Models;
using Service.ApiModels.Order;
using Service.Interfaces;

namespace Service.Services;

public class OrderService(IServiceProvider serviceProvider) : IOrderService
{
    private readonly IBaseRepository<Order> _orderRepository = serviceProvider.GetRequiredService<IBaseRepository<Order>>();

    private readonly IBaseRepository<OrderDetail> _orderDetailRepository =
        serviceProvider.GetRequiredService<IBaseRepository<OrderDetail>>();
    private readonly IBaseRepository<Voucher> voucheRepository = serviceProvider.GetRequiredService<IBaseRepository<Voucher>>();
    public Task<OrderResponse> CalculateTotalPrice(OrderCalcRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<IList<OrderListResponse>> GetAllOrdersAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponse> GetOrderByIdAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<Order> CreateOrderAsync(OrderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<IList<OrderDetailListResponse>> GetAllOrderDetailsAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<OrderDetailResponse> GetOrderDetailByIdAsync(int userId, int orderDetailId)
    {
        throw new NotImplementedException();
    }
}
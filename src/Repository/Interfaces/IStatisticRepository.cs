using System.Linq.Expressions;
using Repository.Extensions;
using Repository.Models;
using Repository.Models.Identity;

namespace Repository.Interfaces;

public interface IStatisticRepository
{
    Task<int> GetUserStatistic(Expression<Func<UserEntity, bool>> predicate = null);
    Task<int> GetShopStatistic(Expression<Func<Shop, bool>> predicate = null);
    Task<int> GetProductStatistic(Expression<Func<Product, bool>> predicate = null);
    Task<int> GetOrderStatistic(Expression<Func<Order, bool>> predicate = null);
    Task<int> GetOrderShopStatistic(Expression<Func<OrderShop, bool>> predicate = null);
    Task<(decimal, int)> GetTotalRevenue(
        Expression<Func<OrderShop, bool>> predicate = null,
        bool isPlatform = false);

    Task<ShopRevenueStatisticResponse> GetShopRevenueStatisticsAsync(int shopId);
    Task<AdminRevenueStatisticResponse> GetAdminRevenueStatisticsAsync();

    Task<IList<RevenueChartResponse>> GetOrderShopStatistic(
        Expression<Func<OrderShop, bool>> predicate = null,
        bool isPlatform = false
    );
}
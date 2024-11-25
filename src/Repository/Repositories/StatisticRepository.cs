using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Infrastructure;
using Repository.Interfaces;
using Repository.Models;
using Repository.Models.Identity;

namespace Repository.Repositories;

public class StatisticRepository(IServiceProvider serviceProvider) : IStatisticRepository
{
    private readonly AppDbContext _context = serviceProvider.GetRequiredService<AppDbContext>();

    public async Task<int> GetUserStatistic(Expression<Func<UserEntity, bool>> predicate = null)
    {
        var query = _context.Users.AsNoTracking();
        query = query.Where(e => e.DeletedTime == null);
        query = predicate != null ? query.Where(predicate) : query;
        return await query.CountAsync();
    }

    public async Task<int> GetShopStatistic(Expression<Func<Shop, bool>> predicate = null)
    {
        var query = _context.Shops.AsNoTracking();
        query = query.Where(e => e.DeletedTime == null);
        query = predicate != null ? query.Where(predicate) : query;
        return await query.CountAsync();
    }

    public async Task<int> GetProductStatistic(Expression<Func<Product, bool>> predicate = null)
    {
        var query = _context.Products.AsNoTracking();
        query = query.Where(e => e.DeletedTime == null);
        query = predicate != null ? query.Where(predicate) : query;
        return await query.CountAsync();
    }

    public async Task<int> GetOrderStatistic(Expression<Func<Order, bool>> predicate = null)
    {
        var query = _context.Orders.AsNoTracking();
        query = query.Where(e => e.DeletedTime == null);
        query = predicate != null ? query.Where(predicate) : query;
        return await query.CountAsync();
    }

    public Task<int> GetOrderShopStatistic(Expression<Func<OrderShop, bool>> predicate = null)
    {
        var query = _context.OrderShops.AsNoTracking();
        query = query.Where(e => e.DeletedTime == null);
        query = predicate != null ? query.Where(predicate) : query;
        return query.CountAsync();
    }

    // get total revenue of from order shop, return total revenue (decimal) and total order (int)
    public async Task<(decimal, int)> GetTotalRevenue(
        Expression<Func<OrderShop, bool>> predicate = null,
        bool isPlatform = false
    )
    {
        var query = _context.OrderShops.AsNoTracking();
        query = query.Where(e => e.DeletedTime == null);
        query = predicate != null ? query.Where(predicate) : query;

        if (!isPlatform)
        {
            return (await query.SumAsync(x => x.Commission), await query.CountAsync());
        }

        return (await query.SumAsync(x => x.TotalRentPrice - x.Commission), await query.CountAsync());
    }
}
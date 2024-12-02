using System.Linq.Expressions;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Repository.Extensions;
using Repository.Infrastructure;
using Repository.Interfaces;
using Repository.Models;
using Repository.Models.Identity;
using Serilog;
using Utility.Enum;

namespace Repository.Repositories;

public class StatisticRepository(IServiceProvider serviceProvider) : IStatisticRepository
{
    private readonly AppDbContext _context = serviceProvider.GetRequiredService<AppDbContext>();
    private readonly IMemoryCache _memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();
    private readonly ILogger _logger = serviceProvider.GetRequiredService<ILogger>();

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

    // apply predicate to get total revenue of from order shop
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

    public async Task<AdminRevenueStatisticResponse> GetAdminRevenueStatisticsAsync()
    {
        // Raw SQL for optimized aggregation
        string sqlQuery = @"
        SELECT 
        SUM(CASE WHEN ""Status"" = @completedStatus THEN ""Commission"" ELSE 0 END) AS TotalRevenue,
        COUNT(CASE WHEN ""Status"" = @completedStatus THEN 1 ELSE NULL END) AS TotalOrder,
        (SELECT COUNT(*) FROM ""Shops"" WHERE ""DeletedTime"" IS NULL) AS TotalShop,
        (SELECT COUNT(*) FROM ""Products"" WHERE ""DeletedTime"" IS NULL) AS TotalProduct,
        (SELECT COUNT(*) FROM ""Users"" WHERE ""DeletedTime"" IS NULL) AS TotalUser
        FROM ""OrderShops""
        WHERE ""DeletedTime"" IS NULL";

        // Pass parameters as an anonymous object
        var parameters = new { completedStatus = (int)OrderShopStatusEnum.Completed };

        using var connection = _context.Database.GetDbConnection();
        await connection.OpenAsync();

        // Execute the query
        _logger.Information("GetAdminRevenueStatisticsAsync: {sqlQuery}, {@parameters}", sqlQuery, parameters);
        var revenueStats = await connection.QueryFirstOrDefaultAsync<AdminRevenueStatisticResponse>(sqlQuery, parameters);
        return revenueStats;
    }


    // get total revenue of from order shop for shop owner
    public async Task<ShopRevenueStatisticResponse> GetShopRevenueStatisticsAsync(int shopId)
    {
        // Raw SQL for optimized aggregation
        string sqlQuery = @"
        SELECT
        SUM(CASE WHEN ""Status"" = @completedStatus THEN ""TotalShopReceive"" ELSE 0 END) AS TotalRevenue,
        COUNT(CASE WHEN ""Status"" = @completedStatus THEN 1 ELSE NULL END) AS TotalOrder,
        (SELECT COUNT(*) FROM ""Products"" WHERE ""ShopId"" = @shopId AND ""DeletedTime"" IS NULL) AS TotalProduct
        FROM ""OrderShops""
        WHERE ""ShopId"" = @shopId AND ""DeletedTime"" IS NULL";

        var parameters = new { completedStatus = (int)OrderShopStatusEnum.Completed, shopId };

        using var connection = _context.Database.GetDbConnection();
        await connection.OpenAsync();

        _logger.Information("GetShopRevenueStatisticsAsync: {sqlQuery}, {@parameters}", sqlQuery, parameters);
        var revenueStats = await connection.QueryFirstOrDefaultAsync<ShopRevenueStatisticResponse>(sqlQuery, parameters);
        return revenueStats;
    }

    public async Task<IList<RevenueChartResponse>> GetOrderShopStatistic(
        Expression<Func<OrderShop, bool>> predicate = null,
        bool isPlatform = false
        )
    {
        var query = _context.OrderShops.AsNoTracking();
        query = query.Where(e => e.DeletedTime == null);
        query = predicate != null ? query.Where(predicate) : query;

        if (!isPlatform)
        {
            return await query
                .GroupBy(x => new { x.CreatedTime.Year, x.CreatedTime.Month })
                .Select(x => new RevenueChartResponse
                {
                    Year = x.Key.Year,
                    Month = x.Key.Month,
                    TotalRevenue =  x.Sum(x => x.TotalShopReceive.Value),
                    TotalOrder = x.Count()
                })
                .ToListAsync();
        }

        return await query
            .GroupBy(x => new { x.CreatedTime.Year, x.CreatedTime.Month })
            .Select(x => new RevenueChartResponse
            {
                Year = x.Key.Year,
                Month = x.Key.Month,
                TotalRevenue = x.Sum(x => x.Commission),
                TotalOrder = x.Count()
            })
            .ToListAsync();


    }
}
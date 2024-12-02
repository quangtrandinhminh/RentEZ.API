using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using Repository.Extensions;
using Repository.Interfaces;
using Repository.Models;
using Repository.Repositories;
using Serilog;
using Service.ApiModels.Statistic;
using Service.Helpers;
using Service.Interfaces;
using Service.Mapper;
using Service.Utils;
using Utility.Constants;
using Utility.Enum;
using Utility.Exceptions;
using Utility.Helpers;

namespace Service.Services;

public class StatisticServices(IServiceProvider serviceProvider) : IStatisticService
{
    private readonly ILogger _logger = serviceProvider.GetRequiredService<ILogger>();
    private readonly MapperlyMapper _mapper = serviceProvider.GetRequiredService<MapperlyMapper>();
    private readonly IStatisticRepository _statisticRepository = serviceProvider.GetRequiredService<IStatisticRepository>();
    private readonly IHttpContextAccessor _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    private readonly TimeSpan _cacheTime = TimeSpan.FromMinutes(10);
    private readonly IShopRepository _shopRepository = serviceProvider.GetRequiredService<IShopRepository>();

    public async Task<int> GetUserStatistic()
    {
        _logger.Information("GetUserStatistic");
        var result = await _statisticRepository.GetUserStatistic();
        return result;
    }

    public async Task<int> GetShopStatistic()
    {
        _logger.Information("GetShopStatistic");
        var result = await _statisticRepository.GetShopStatistic();
        return result;
    }

    public async Task<int> GetProductStatistic()
    {
        _logger.Information("GetProductStatistic");
        var user = _httpContextAccessor.HttpContext.User;
        var role = JwtClaimUltils.GetUserRole(user);
        var userId = JwtClaimUltils.GetUserId(user);
        var result = 0;

        if (role.Contains(UserRoleEnum.ShopOwner.ToString()))
        {
            var shop = await _shopRepository.GetSingleAsync(x => x.OwnerId == userId);
            if (shop == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsShop.NOTFOUND,
                    StatusCodes.Status400BadRequest);
            }

            result = await _statisticRepository.GetProductStatistic(x => x.ShopId == shop.Id);
            return result;
        }

        result = await _statisticRepository.GetProductStatistic();
        return result;
    }

    public async Task<int> GetOrderStatistic()
    {
        _logger.Information("GetOrderStatistic");
        var result = await _statisticRepository.GetOrderStatistic(x => x.PaymentStatus == PaymentStatusEnum.Paid);
        return result;
    }

    public async Task<RevenueStatisticResponse> GetTotalRevenue(StatisticRequest request)
    {
        var fromDate = request.FromDateTimeOffset.ToUniversalTime();
        var toDate = request.FromDateTimeOffset.AddDays(request.DayNumber);
        _logger.Information($"GetTotalRevenue from {fromDate} to {toDate}");
        var user = _httpContextAccessor.HttpContext.User;
        var role = JwtClaimUltils.GetUserRole(user);
        var userId = JwtClaimUltils.GetUserId(user);
        RevenueStatisticResponse result = new();
        result.From = fromDate;
        result.To = toDate;

        Expression<Func<OrderShop, bool>> predicate =
            x => x.Status == OrderShopStatusEnum.Completed &&
                x.CreatedTime >= request.FromDateTimeOffset &&
                x.CreatedTime <= request.FromDateTimeOffset.AddDays(request.DayNumber);

        // If user is shop owner, get revenue of his shop - (total rent price - commission)
        if (role.Contains(UserRoleEnum.ShopOwner.ToString()))
        {
            var shop = await _shopRepository.GetSingleAsync(x => x.OwnerId == userId);
            if (shop == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsShop.NOTFOUND,
                                       StatusCodes.Status400BadRequest);
            }

            result.ShopId = shop.Id;
            predicate = predicate.And(x => x.ShopId == shop.Id);
            (result.TotalRevenue, result.TotalOrderShop) = await _statisticRepository.GetTotalRevenue(
                               predicate, false
                                              );
        }

        // If user is admin, get revenue of platform - commission
        (result.TotalRevenue, result.TotalOrderShop) = await _statisticRepository.GetTotalRevenue(
            predicate, true
            );
        return result;
    }

    public async Task<AdminRevenueStatisticResponse> GetAdminRevenueStatisticsAsync()
    {
        _logger.Information($"GetAdminRevenueStatisticsAsync at {CoreHelper.SystemTimeNow}");
        return await _statisticRepository.GetAdminRevenueStatisticsAsync();
    }

    public async Task<ShopRevenueStatisticResponse> GetShopRevenueStatisticsAsync()
    {
        var user = JwtClaimUltils.GetLoginedUser(_httpContextAccessor);
        var userid = JwtClaimUltils.GetUserId(user);
        var shop = await _shopRepository.GetSingleAsync(x => x.OwnerId == userid);
        if (shop == null)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsShop.NOTFOUND,
                               StatusCodes.Status400BadRequest);
        }

        _logger.Information($"GetShopRevenueStatisticsAsync at {CoreHelper.SystemTimeNow}");
        return await _statisticRepository.GetShopRevenueStatisticsAsync(shop.Id);
    }

    public async Task<IList<RevenueChartResponse>> GetRevenueChart(RevenueChartRequest request)
    {
        var user = JwtClaimUltils.GetLoginedUser(_httpContextAccessor);
        var role = JwtClaimUltils.GetUserRole(user);
        var userId = JwtClaimUltils.GetUserId(user);
        var year = request.Year;
        int? month = request.Month;
        _logger.Information($"Get Revenue Chart in year {year} and month {month}");
        Expression<Func<OrderShop, bool>> predicate =
            x => x.CreatedTime.Year == year && x.Status == OrderShopStatusEnum.Completed;

        var isPlatform = true;

        if (month.HasValue)
        {
            predicate = predicate.And(x => x.CreatedTime.Month == month.Value);
        }

        if (role.Contains(UserRoleEnum.ShopOwner.ToString()))
        {
            var shop = await _shopRepository.GetSingleAsync(x => x.OwnerId == userId);
            if (shop == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsShop.NOTFOUND,
                                       StatusCodes.Status400BadRequest);
            }

            predicate = predicate.And(x => x.ShopId == shop.Id);
            isPlatform = false;
        }

        var result = await _statisticRepository.GetOrderShopStatistic(predicate, isPlatform);

        if (!month.HasValue)
        {
            // Add logic to ensure the result contains all 12 months
            result = Enumerable.Range(1, 12)
                .Select(month => new RevenueChartResponse
                {
                    Year = year,
                    Month = month,
                    TotalRevenue = result.FirstOrDefault(r => r.Month == month)?.TotalRevenue ?? 0,
                    TotalOrder = result.FirstOrDefault(r => r.Month == month)?.TotalOrder ?? 0
                })
                .ToList();
        }

        return result;
    }
}
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Repository.Models;
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
}
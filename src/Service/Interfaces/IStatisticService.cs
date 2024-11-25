using Service.ApiModels.Statistic;

namespace Service.Interfaces;

public interface IStatisticService
{
    Task<int> GetUserStatistic();
    Task<int> GetShopStatistic();   
    Task<int> GetProductStatistic();
    Task<int> GetOrderStatistic();
    Task<RevenueStatisticResponse> GetTotalRevenue(StatisticRequest request);
}
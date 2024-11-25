using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ApiModels;
using Service.ApiModels.Statistic;
using Service.Interfaces;
using Utility.Helpers;

namespace RentEZ.WebAPI.Controllers
{
    [Route("api/stats")]
    [ApiController]
    public class StatsController(IServiceProvider serviceProvider) : ControllerBase
    {
        private readonly IStatisticService _statisticService = serviceProvider.GetRequiredService<IStatisticService>();

        [HttpGet("admin/user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserStatistic()
        {
            var result = await _statisticService.GetUserStatistic();
            return Ok(BaseResponse.OkResponseDto(result, $"Last Updated: {CoreHelper.SystemTimeNow}"));
        }

        [HttpGet("admin/shop")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetShopStatistic()
        {
            var result = await _statisticService.GetShopStatistic();
            return Ok(BaseResponse.OkResponseDto(result, $"Last Updated: {CoreHelper.SystemTimeNow}"));
        }

        [HttpGet("admin/order")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderStatistic()
        {
            var result = await _statisticService.GetOrderStatistic();
            return Ok(BaseResponse.OkResponseDto(result, $"Last Updated: {CoreHelper.SystemTimeNow}"));
        }

        [HttpGet("product")]
        [Authorize(Roles = "Admin, ShopOwner")]
        public async Task<IActionResult> GetProductStatistic()
        {
            var result = await _statisticService.GetProductStatistic();
            return Ok(BaseResponse.OkResponseDto(result, $"Last Updated: {CoreHelper.SystemTimeNow}"));
        }

        [HttpGet("revenue")]
        [Authorize(Roles = "Admin, ShopOwner")]
        public async Task<IActionResult> GetTotalRevenue([FromQuery] StatisticRequest request)
        {
            var result = await _statisticService.GetTotalRevenue(request);
            return Ok(BaseResponse.OkResponseDto(result, $"Last Updated: {CoreHelper.SystemTimeNow}"));
        }
    }
}

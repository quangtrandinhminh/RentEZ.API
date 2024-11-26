using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ApiModels;
using Service.ApiModels.OrderDetail;
using Service.Interfaces;
using Utility.Constants;

namespace RentEZ.WebAPI.Controllers
{
    [Route("api/feedbacks")]
    [ApiController]
    public class FeedbackController(IServiceProvider serviceProvider) : ControllerBase
    {
        private readonly IOrderService _orderService = serviceProvider.GetRequiredService<IOrderService>();

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateFeedback([FromBody] FeedbackRequest request)
        {
            await _orderService.CreateFeedback(request);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpPut]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateFeedback([FromBody] FeedbackRequest request)
        {
            await _orderService.EditFeedback(request);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedbackList([FromQuery] FeedbackListRequest request)
        {
            var response = await _orderService.GetFeedbackList(request);
            return Ok(BaseResponse.OkResponseDto(response));
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ApiModels.Order;
using Service.ApiModels.OrderShop;
using Service.ApiModels.VNPay;
using Service.Interfaces;
using Utility.Enum;
using BaseResponse = Service.ApiModels.BaseResponse;

namespace RentEZ.WebAPI.Controllers
{
    [Authorize]
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("admin")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _orderService.GetAllOrdersAsync();
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var response = await _orderService.GetOrderByIdAsync(id);
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpPost("cart/calculation")]
        [AllowAnonymous]
        public async Task<IActionResult> CalculateTotalPrice([FromBody] CartCalcRequest request)
        {
            var response = await _orderService.CalculateTotalPrice(request);
            return Created(nameof(CalculateTotalPrice), BaseResponse.OkResponseDto(response));
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            var response = await _orderService.CreateOrderAsync(request);
            return Created(nameof(CreateOrder), BaseResponse.OkResponseDto("OrderId: " +response));
        }

        [HttpGet("order-shops")]
        [Authorize(Roles="Customer, ShopOwner")]
        public async Task<IActionResult> GetAllOrderShops([FromQuery] OrderShopListRequest request)
        {
            var response = await _orderService.GetAllOrderShopsAsync(request);
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpGet("order-shops/{orderShopId}")]
        [Authorize(Roles="Customer, ShopOwner")]
        public async Task<IActionResult> GetOrderShopById([FromRoute] int orderShopId)
        {
            var response = await _orderService.GetOrderShopsByIdAsync(orderShopId);
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpDelete("{orderId}")]
        [Authorize(Roles = "ShopOwner, Customer")]
        public async Task<IActionResult> CancelOrder([FromRoute] int orderId)
        {
            await _orderService.CancelOrderAsync(orderId);
            return Ok(BaseResponse.OkResponseDto("Order canceled successfully"));
        }
    }
}

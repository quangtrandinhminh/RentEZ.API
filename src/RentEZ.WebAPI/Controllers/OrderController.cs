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

        [HttpGet("payment-methods")]
        public IActionResult GetAllPaymentEnum()
        {
            var response = _orderService.GetAllPaymentEnum();
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> GetAllOrders([FromQuery] OrderListRequest request)
        {
            var response = await _orderService.GetAllOrdersAsync(request);
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Customer")]
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

        [HttpGet("order-shops/enums")]
        public IActionResult GetAllOrderShopsEnum()
        {
            var response = _orderService.GetAllOrderShopStatusEnumV2();
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpGet("v2/order-shops/enums")]
        public IActionResult GetAllOrderShopsEnumV2()
        {
            var response = _orderService.GetAllOrderShopStatusEnumV2();
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpGet("order-shops")]
        public async Task<IActionResult> GetAllOrderShops([FromQuery] OrderShopListRequest request)
        {
            var response = await _orderService.GetAllOrderShopsAsync(request);
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpGet("order-shops/{orderShopId}")]
        public async Task<IActionResult> GetOrderShopById([FromRoute] int orderShopId, [FromQuery] bool isShop = false)
        {
            var response = await _orderService.GetOrderShopsByIdAsync(orderShopId, isShop);
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpPut("order-shops/{orderShopId}/status")]
        [Authorize(Roles = "Customer, ShopOwner")]
        public async Task<IActionResult> ChangeOrderStatus([FromRoute] int orderShopId, [FromQuery] bool isShop = false)
        {
            var status = await _orderService.ChangeOrderShopStatusAsync(orderShopId, isShop);
            return Ok(BaseResponse.OkResponseDto($"Order status changed into status [{status}] successfully"));
        }

        [HttpDelete("{orderId}")]
        [Authorize(Roles = "ShopOwner, Customer")]
        public async Task<IActionResult> CancelOrder([FromRoute] int orderId)
        {
            await _orderService.CancelOrderAsync(orderId);
            return Ok(BaseResponse.OkResponseDto("Order canceled successfully"));
        }

        [HttpPut("order-shops/{orderShopId}/refund-status")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ChangeToWaitingToRefund([FromRoute] int orderShopId)
        {
            await _orderService.ChangeToWaitingForRefund(orderShopId);
            return Ok(BaseResponse.OkResponseDto("Order status changed successfully"));
        }
    }
}

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ApiModels;
using Service.ApiModels.Order;
using Service.Interfaces;
using Service.Models;

namespace RentEZ.WebAPI.Controllers
{
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
            var userId = Extensions.ClaimExtension.GetUserId(User);
            var response = await _orderService.GetAllOrdersAsync(userId);
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var response = await _orderService.GetOrderByIdAsync(id);
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpPost("calculation")]
        public async Task<IActionResult> CalculateTotalPrice([FromBody] OrderCalcRequest request)
        {
            var response = await _orderService.CalculateTotalPrice(request);
            return Created(nameof(CalculateTotalPrice), BaseResponse.OkResponseDto(response));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            var response = await _orderService.CreateOrderAsync(request);
            return Created(nameof(CreateOrder), BaseResponse.OkResponseDto(response));
        }

        [HttpGet("order-details")]
        [Authorize(Roles="Customer, ShopOwner")]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var userId = Extensions.ClaimExtension.GetUserId(User);
            var response = await _orderService.GetAllOrderDetailsAsync(userId);
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpGet("order-details/{orderDetailId}")]
        [Authorize(Roles="Customer, ShopOwner")]
        public async Task<IActionResult> GetOrderDetailById(int orderDetailId)
        {
            var userId = Extensions.ClaimExtension.GetUserId(User);
            var response = await _orderService.GetOrderDetailByIdAsync(userId, orderDetailId);
            return Ok(BaseResponse.OkResponseDto(response));
        }
    }
}

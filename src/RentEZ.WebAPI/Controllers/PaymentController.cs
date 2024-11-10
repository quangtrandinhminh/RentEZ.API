using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Service.ApiModels.VNPay;
using Service.ApiModels;
using Service.ApiModels.PayOS;
using Service.Interfaces;
using Utility.Constants;
using Service.ApiModels.Refund;
using Service.Services;
using Utility.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RentEZ.WebAPI.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IPayOSService _payosService;
        private readonly IRefundService _refundService;
        public PaymentController(IVnPayService vnPayService, IPayOSService payosService, IRefundService refundService)
        {
            _vnPayService = vnPayService;
            _payosService = payosService;
            _refundService = refundService;
        }

        [HttpPost("vnpay/payment-url")]
        public async Task<IActionResult> GetVnPayPaymentUrl([FromBody] VnPaymentRequest request)
        {
            var response = await _vnPayService.CreatePaymentUrl(HttpContext, request);
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpPost("vnpay/payment-execute")]
        public async Task<IActionResult> VnPayPaymentExecute(HttpContext context, int orderId)
        {
            var response = await _vnPayService.PaymentExecute(context, orderId);
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpPost("payos/payment-url")]
        public async Task<IActionResult> GetPayOSPaymentUrl([FromBody] PayOSRequest request)
        {
            var response = await _payosService.CreatePaymentLinkAsync(request);
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpPost("payos/transfer-handler")]
        public async Task<IActionResult> PayOSPaymentExecute(WebhookType request)
        {
            await _payosService.GetWebhookTypeAsync(request);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpGet("payos/payment-info")]
        public async Task<IActionResult> GetPayOSPaymentInfo([FromQuery] int orderId)
        {
            var response = await _payosService.GetPaymentLinkInformationAsync(orderId);
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpPost("refund")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProcessRefund([FromBody] RefundRequest request)
        {
            try
            {
                var (customerQR, shopQR) = await _refundService.ProcessRefundAsync(request);
                return Ok(new { CustomerRefundQR = customerQR, ShopPaymentQR = shopQR });
            }
            catch (AppException ex)
            {
                return StatusCode(ex.StatusCode, new { ex.Code, ex.Message });
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Service.ApiModels.VNPay;
using Service.ApiModels;
using Service.ApiModels.PayOS;
using Service.Interfaces;
using Utility.Constants;

namespace RentEZ.WebAPI.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IPayOSService _payosService;

        public PaymentController(IVnPayService vnPayService, IPayOSService payosService)
        {
            _vnPayService = vnPayService;
            _payosService = payosService;
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

        [HttpGet("payos/payment-info/{orderId}")]
        public async Task<IActionResult> GetPayOSPaymentInfo([FromRoute] int orderId)
        {
            var response = await _payosService.GetPaymentLinkInformationAsync(orderId);
            return Ok(BaseResponse.OkResponseDto(response));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Service.ApiModels.VNPay;
using Service.ApiModels;
using Service.ApiModels.PayOS;
using Service.Interfaces;
using Utility.Constants;
using Service.ApiModels.Refund;
using Utility.Exceptions;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Roles = "Customer")]
        [HttpPost("payos/customer/payment-url")]
        public async Task<IActionResult> GetPayOSPaymentUrl([FromBody] PayOSRequest request)
        {
            var response = await _payosService.CreatePaymentLinkAsync(request);
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [HttpPost("payos/transfer-handler")]
        public IActionResult PayOSPaymentExecute([FromBody] WebhookType request)
        {
            _payosService.GetWebhookTypeAsync(request);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("payos/admin/payment-info/{orderCode}")]
        public async Task<IActionResult> GetPayOSPaymentInfo([FromRoute] long orderCode)
        {
            var response = await _payosService.GetPaymentLinkInformationAsync(orderCode);
            return Ok(BaseResponse.OkResponseDto(response));
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("payos/customer/confirmation/{orderId}/{orderCode}")]
        public async Task<IActionResult> PayOSConfirmPayment([FromRoute] int orderId, long orderCode)
        {
            await _payosService.ConfirmPayment(orderId, orderCode);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
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

        [Authorize(Roles = "Admin")]
        [HttpPost("payos/admin/webhook/confirmation")]
        public async Task<IActionResult> PayOSWebhookConfirmation([FromBody] ConfirmWebhook request)
        {
            await _payosService.ConfirmWebhook(request.webhook_url);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }
    }
}

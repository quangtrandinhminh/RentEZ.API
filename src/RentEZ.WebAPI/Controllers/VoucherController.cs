using Microsoft.AspNetCore.Mvc;
using Service.ApiModels;
using Service.ApiModels.Voucher;
using Service.Interfaces;
using Utility.Constants;

namespace RentEZ.WebAPI.Controllers
{
    [ApiController]
    [Route("api/vouchers")]
    public class VoucherController : Controller
    {
        private IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVouchers([FromQuery] VoucherQueryRequest queryRequest)
        {
            var vouchers = await _voucherService.GetAllVoucherAsync(queryRequest);
            return Ok(BaseResponse.OkResponseDto(vouchers));
        }

        [HttpGet("{voucherId}")]
        public async Task<IActionResult> GetVoucherById(int voucherId)
        {
            var voucher = await _voucherService.GetVoucherByIdAsync(voucherId);
            return Ok(BaseResponse.OkResponseDto(voucher));
        }

        [HttpPost]
        public async Task<IActionResult> CreateVoucher([FromBody] VoucherRequest request)
        {
            var voucher = await _voucherService.CreateVoucherAsync(request);
            return Ok(BaseResponse.OkResponseDto(voucher));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVoucher([FromBody] VoucherOrderRequest request)
        {
            await _voucherService.UpdateVoucherAsync(request);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpDelete("{voucherId}")]
        public async Task<IActionResult> DeleteVoucher(int voucherId)
        {
            await _voucherService.DeleteVoucherAsync(voucherId);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }
    }
}

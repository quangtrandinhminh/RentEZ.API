using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ApiModels;
using Service.ApiModels.Voucher;
using Service.Interfaces;
using Utility.Constants;
using Utility.Enum;

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

        [HttpGet("enums")]
        public IActionResult GetAllVoucherEnums()
        {
            var enums = _voucherService.GetAllVoucherEnum();
            return Ok(BaseResponse.OkResponseDto(enums));
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
        [Authorize(Roles = "Admin, ShopOwner")]
        public async Task<IActionResult> CreateVoucher([FromBody] VoucherRequest request)
        {
            var voucher = await _voucherService.CreateVoucherAsync(request);
            return Ok(BaseResponse.OkResponseDto(voucher));
        }

        [HttpPut]
        [Authorize(Roles = "Admin, ShopOwner")]
        public async Task<IActionResult> UpdateVoucher([FromBody] VoucherOrderRequest request)
        {
            await _voucherService.UpdateVoucherAsync(request);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpDelete("{voucherId}")]
        [Authorize(Roles = "Admin, ShopOwner")]
        public async Task<IActionResult> DeleteVoucher(int voucherId)
        {
            await _voucherService.DeleteVoucherAsync(voucherId);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }
    }
}

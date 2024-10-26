using Microsoft.AspNetCore.Mvc;
using Service.ApiModels;
using Service.ApiModels.Voucher;
using Service.Interfaces;

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
        public async Task<IActionResult> GetAllVouchers([FromQuery] int pageNumber = 1, int pageSize = 10)
        {
            var vouchers = await _voucherService.GetAllVoucherAsync(pageNumber, pageSize);
            return Ok(BaseResponse.OkResponseDto(vouchers));
        }
    }
}

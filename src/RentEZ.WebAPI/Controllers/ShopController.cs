using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Utility.Constants;
using Microsoft.AspNetCore.RateLimiting;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;
using System.ComponentModel.DataAnnotations;
using BaseResponse = Service.ApiModels.BaseResponse;
using Service.ApiModels.Shop;

namespace RentEZ.WebAPI.Controllers
{
    [Route("api/shops")]
    [ApiController]
    [EnableRateLimiting("EndpointRateLimitPolicy")]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpPost()]
        [Authorize(Roles = "ShopOwner")]
        public async Task<IActionResult> CreateNewShop([FromBody] ShopCreateRequest request)
        {
            await _shopService.CreateShop(request);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpGet("admin/pending")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPendingShops()
        {
            var shops = await _shopService.GetPendingShopList();
            return Ok(BaseResponse.OkResponseDto(shops));
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetShopById([FromRoute]int id)
        {
            var shop = await _shopService.GetShopById(id);
            return Ok(BaseResponse.OkResponseDto(shop));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllShops([FromQuery] ShopQueryParams queryParams)
        {
            var shops = await _shopService.GetAllShops(queryParams);
            return Ok(BaseResponse.OkResponseDto(shops));
        }

        [HttpPut("admin/approval")]   
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShopApproval([Required]int id)
        {
            await _shopService.ShopToApprove(id);
            return Ok(BaseResponse.OkResponseDto("Shop approved successfully"));
        }
    }
}

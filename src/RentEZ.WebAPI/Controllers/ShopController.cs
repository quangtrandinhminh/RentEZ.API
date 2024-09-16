using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Utility.Constants;
using Microsoft.AspNetCore.RateLimiting;
using Service.Models;
using Service.Models.Shop;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;

namespace RentEZ.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("EndpointRateLimitPolicy")]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpPost]
        [Authorize(Roles = "ShopOwner")]
        [Route("create-new-shop")]
        public async Task<IActionResult> CreateNewShop([Microsoft.AspNetCore.Mvc.FromBody] ShopCreateRequest request)
        {
            await _shopService.CreateShop(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("get-all-pending-shops")]
        public async Task<IActionResult> GetAllPendingShops()
        {
            var shops = await _shopService.GetPendingShopList();
            return Ok(BaseResponseDto.OkResponseDto(shops));
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [AllowAnonymous]
        [Route("get-all-shops")]
        public async Task<IActionResult> GetAllShops()
        {
            var shops = await _shopService.GetAllShops();
            return Ok(BaseResponseDto.OkResponseDto(shops));
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("shop-approval")]
        public async Task<IActionResult> ShopApproval(int id)
        {
            await _shopService.ShopToApprove(id);
            return Ok(BaseResponseDto.OkResponseDto("Shop approved successfully"));
        }
    }
}

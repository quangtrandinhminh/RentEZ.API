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
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpGet("admin/pending")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPendingShops()
        {
            var shops = await _shopService.GetPendingShopList();
            return Ok(BaseResponseDto.OkResponseDto(shops));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllShops()
        {
            var shops = await _shopService.GetAllShops();
            return Ok(BaseResponseDto.OkResponseDto(shops));
        }

        [HttpPut("admin/approval")]   
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShopApproval([Required]int id)
        {
            await _shopService.ShopToApprove(id);
            return Ok(BaseResponseDto.OkResponseDto("Shop approved successfully"));
        }
    }
}

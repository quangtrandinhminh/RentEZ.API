using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Utility.Constants;
using System.Web.Http;
using Microsoft.AspNetCore.RateLimiting;
using BusinessObject.DTO.Shop;


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

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [AllowAnonymous]
        [Route("create-new-shop")]
        public async Task<IActionResult> CreateNewShop([Microsoft.AspNetCore.Mvc.FromBody] ShopCreateRequestDto request)
        {
            await _shopService.CreateShop(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [AllowAnonymous]
        [Route("get-all-shops")]
        public async Task<IActionResult> GetAllShops()
        {
            var shops = await _shopService.GetAllShops();
            return Ok(BaseResponseDto.OkResponseDto(shops));
        }
    }
}

using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Web.Http;

namespace RentEZ.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [AllowAnonymous]
        [Route("get-product-by-id")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            return Ok(BaseResponseDto.OkResponseDto(product));
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [AllowAnonymous]
        [Route("get-all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(BaseResponseDto.OkResponseDto(products));
        }
    }
}

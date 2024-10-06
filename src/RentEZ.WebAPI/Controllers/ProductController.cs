using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;
using Service.Models;
using Service.Models.Product;
using Utility.Constants;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;

namespace RentEZ.WebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            return Ok(BaseResponseDto.OkResponseDto(product));
        }

        [HttpGet("category")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProducts([FromQuery] int? categoryId = null)
        {
            var products = await _productService.GetAllProducts(categoryId);
            return Ok(BaseResponseDto.OkResponseDto(products));
        }

        [HttpPost()]
        [Authorize(Roles = "ShopOwner")]
        public async Task<IActionResult> CreateNewProduct([FromBody] ProductCreateRequestDto request)
        {
            await _productService.CreateProduct(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ShopOwner")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductCreateRequestDto request, [FromRoute] int id)
        {
            await _productService.UpdateProductAsync(request, id);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ShopOwner")]
        public async Task<IActionResult> DeleteShop([FromRoute] int id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }
    }
}

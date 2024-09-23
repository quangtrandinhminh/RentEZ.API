using Microsoft.AspNetCore.Mvc;
﻿using BusinessObject.DTO;
using BusinessObject.DTO.Product;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Services;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;
using Service.Models;
using Utility.Constants;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;

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

        [HttpGet]
        [AllowAnonymous]
        [Route("get-product-by-id")]
        public async Task<IActionResult> GetProductById([Required]int id)
        {
            var product = await _productService.GetProductById(id);
            return Ok(BaseResponseDto.OkResponseDto(product));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get-all-products")]
        public async Task<IActionResult> GetAllProducts([FromQuery] int? categoryId = null)
        {
            var products = await _productService.GetAllProducts(categoryId);
            return Ok(BaseResponseDto.OkResponseDto(products));
        }

        [HttpPost]
        //[Authorize(Roles = "ShopOwner")]
        [Route("create-new-product")]
        public async Task<IActionResult> CreateNewProduct([FromBody] ProductCreateRequestDto request)
        {
            await _productService.CreateProduct(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpPut]
        //[Authorize(Roles = "ShopOwner")]
        [Route("update-product")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductCreateRequestDto request, [Required]int id)
        {
            await _productService.UpdateProductAsync(request, id);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpDelete]
        //[Authorize(Roles = "ShopOwner")]
        [Route("delete-product")]
        public async Task<IActionResult> DeleteShop([Required]int id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }
    }
}

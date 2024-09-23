using BusinessObject.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;
using Service.Models.Category;
using System.ComponentModel.DataAnnotations;
using Utility.Constants;

namespace RentEZ.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("get-all-categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(BaseResponseDto.OkResponseDto(categories));
        }

        [HttpGet]
        [Route("get-category-by-id")]
        public async Task<IActionResult> GetCategory([Required]int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            return Ok(BaseResponseDto.OkResponseDto(category));
        }

        [HttpPost]
        [Route("create-new-category")]
        public async Task<IActionResult> CreateCategory([FromBody]CategoryCreateRequest request)
        {
            await _categoryService.CreateCategoryAsync(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpPut]
        [Route("update-category")]
        public async Task<IActionResult> UpdateCategory([FromBody]CategoryCreateRequest request, [Required] int id)
        {
            await _categoryService.UpdateCategoryAsync(request, id);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpDelete]
        [Route("delete-category")]
        public async Task<IActionResult> DeleteCategory([Required]int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }
    }
}

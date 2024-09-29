using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;
using Service.Models.Category;
using System.ComponentModel.DataAnnotations;
using Utility.Constants;

namespace RentEZ.WebAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController(IServiceProvider serviceProvider) : ControllerBase
    {
        private readonly ICategoryService _categoryService = serviceProvider.GetRequiredService<ICategoryService>();

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(BaseResponseDto.OkResponseDto(categories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute]int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            return Ok(BaseResponseDto.OkResponseDto(category));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]CategoryCreateRequest request)
        {
            await _categoryService.CreateCategoryAsync(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody]CategoryCreateRequest request, [FromRoute] int id)
        {
            await _categoryService.UpdateCategoryAsync(request, id);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute]int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }
    }
}

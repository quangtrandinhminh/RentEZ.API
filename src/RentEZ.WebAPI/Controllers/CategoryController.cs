using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;
using Service.Models.Category;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;
using Utility.Constants;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;

namespace RentEZ.WebAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

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

        [HttpPost()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody]CategoryCreateRequest request)
        {
            await _categoryService.CreateCategoryAsync(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromBody]CategoryCreateRequest request, [FromRoute] int id)
        {
            await _categoryService.UpdateCategoryAsync(request, id);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory([FromRoute]int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }
    }
}

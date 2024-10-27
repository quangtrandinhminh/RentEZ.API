using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Web.Http;
using Utility.Constants;
using BaseResponse = Service.ApiModels.BaseResponse;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using Service.ApiModels.Category;

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
            return Ok(BaseResponse.OkResponseDto(categories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute]int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            return Ok(BaseResponse.OkResponseDto(category));
        }

        [HttpPost()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody]CategoryCreateRequest request)
        {
            await _categoryService.CreateCategoryAsync(request);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromBody]CategoryCreateRequest request, [FromRoute] int id)
        {
            await _categoryService.UpdateCategoryAsync(request, id);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory([FromRoute]int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }
    }
}

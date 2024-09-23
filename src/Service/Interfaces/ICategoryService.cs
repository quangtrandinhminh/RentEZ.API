using BusinessObject.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetAllCategoriesAsync();
        Task<CategoryResponseDto> GetCategoryById(int id);
        Task CreateCategoryAsync(CategoryRequestDto request, CancellationToken cancellationToken = default);
        Task UpdateCategoryAsync(CategoryRequestDto request, int id);
        Task DeleteCategoryAsync(int id);
    }
}

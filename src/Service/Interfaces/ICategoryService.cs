using Service.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategoriesAsync();
        Task<CategoryResponse> GetCategoryById(int id);
        Task CreateCategoryAsync(CategoryCreateRequest request, CancellationToken cancellationToken = default);
        Task UpdateCategoryAsync(CategoryCreateRequest request, int id);
        Task DeleteCategoryAsync(int id);
    }
}

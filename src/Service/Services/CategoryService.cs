using Microsoft.AspNetCore.Http;
using Serilog;
using Repository.Infrastructure;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;
using Utility.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Service.Mapper;
using Service.Models.Category;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly MapperlyMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUserRepository userRepository, MapperlyMapper mapper, ILogger logger, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
        {
            _logger.Information($"Get all categories");
            var categories = await _categoryRepository.GetAllWithCondition().ToListAsync();
            if (categories.IsNullOrEmpty())
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsCategory.NOTFOUND, StatusCodes.Status404NotFound);
            }
            return _mapper.CategoriesToCategoriesResponseDto(categories).ToList();
        }

        public async Task<CategoryResponse> GetCategoryById(int id)
        {
            _logger.Information($"Get category by id {id}");
            var category = await _categoryRepository.GetByIdAsync(id);
            if(category == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsCategory.NOTFOUND, StatusCodes.Status404NotFound);
            }
            return _mapper.CategoryToCategoryResponseDto(category);
        }

        public async Task CreateCategoryAsync(CategoryCreateRequest request, CancellationToken cancellationToken = default)
        {
            _logger.Information($"Create new category");
            var category = await _categoryRepository.GetSingleAsync(x => x.CategoryName == request.CategoryName);
            if(category != null )
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsCategory.EXISTED_CATEGORYNAME, StatusCodes.Status400BadRequest);
            }
            var description = await _categoryRepository.GetSingleAsync(x => x.Description  == request.Description);
            if( description != null )
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsCategory.EXISTED_DESCRIPTION, StatusCodes.Status400BadRequest);
            }
            try
            {
                var newCategory = new Category()
                {
                    CategoryName = request.CategoryName,
                    Description = request.Description,
                    CreatedTime = DateTimeOffset.UtcNow,
                    LastUpdatedTime = DateTimeOffset.UtcNow,
                };

                _mapper.CategoryToCreateCategory(request, newCategory);
                await _categoryRepository.AddAsync(newCategory, cancellationToken);
                await _unitOfWork.SaveChangeAsync();
                _logger.Information("New category created successfully");
            }
            catch (Exception ex)
            {
                throw new AppException(ResponseCodeConstants.FAILED, ex.Message, StatusCodes.Status400BadRequest);
            }
        }

        public async Task UpdateCategoryAsync(CategoryCreateRequest request, int id)
        {
            _logger.Information($"Update category with id {id}");
            var existingCategory = await _categoryRepository.GetSingleAsync(x => x.Id == id);
            if( existingCategory == null )
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsCategory.NOTFOUND, StatusCodes.Status404NotFound);
            }
            
            var categoryWithSameName = await _categoryRepository.GetSingleAsync(x => x.CategoryName == request.CategoryName && x.Id != id);
            if( categoryWithSameName != null )
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsCategory.EXISTED_CATEGORYNAME, StatusCodes.Status400BadRequest);
            }
            var categoryWithSameDescription = await _categoryRepository.GetSingleAsync(x => x.Description ==  request.Description && x.Id != id);
            if( categoryWithSameDescription != null )
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsCategory.EXISTED_DESCRIPTION, StatusCodes.Status400BadRequest);
            }
            if (existingCategory.CategoryName == request.CategoryName && existingCategory.Description == request.Description)
            {

                throw new AppException(ResponseCodeConstants.NOTHINGCHANGED, ResponseMessageConstrantsCategory.NOTHING_CHANGED, StatusCodes.Status400BadRequest);
            }

            try
            {
                existingCategory.CategoryName = request.CategoryName;
                existingCategory.Description = request.Description;
                existingCategory.LastUpdatedTime = DateTimeOffset.UtcNow;

                _mapper.CategoryToCreateCategory(request, existingCategory);
                _categoryRepository.Update(existingCategory);
                await _unitOfWork.SaveChangeAsync();
                _logger.Information("Update category successfully");
            }
            catch( Exception ex )
            {
                throw new AppException(ResponseCodeConstants.FAILED, ex.Message, StatusCodes.Status400BadRequest);
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            _logger.Information($"Delete category with id {id}");
            var existingCategory = await _categoryRepository.GetSingleAsync(x => x.Id == id);
            if(existingCategory == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsCategory.NOTFOUND, StatusCodes.Status404NotFound);
            }
            _categoryRepository.Delete(existingCategory);
            await _unitOfWork.SaveChangeAsync();
            _logger.Information($"Delete category with id {id} successfully");
        }
    }
}   

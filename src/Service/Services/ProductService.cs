using BusinessObject.DTO.Product;
using BusinessObject.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.IdentityModel.Tokens;
using Repository.Infrastructure;
using Repository.Interfaces;
using Service.Interfaces;
using Utility.Constants;
using Utility.Exceptions;
using BusinessObject.DTO.Shop;
using BusinessObject.Entities;
using Repository.Repositories;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly MapperlyMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, MapperlyMapper mapper, ILogger logger, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }
        // get all products
        public async Task<List<ProductResponseDto>> GetAllProducts(int? categoryId = null)
        {
            _logger.Information($"Get all products");
            if (categoryId.HasValue)
            {
                var categoryExists = await _categoryRepository.GetSingleAsync(c => c.Id == categoryId.Value);
                if (categoryExists == null)
                {
                    throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsProduct.NONEXISTENT_CATEGORY, StatusCodes.Status404NotFound);
                }
            }

            var productsWithCategory = _productRepository.GetAllWithCondition();
            productsWithCategory = productsWithCategory.Include(x => x.Category);

            if (categoryId.HasValue)
            {
                productsWithCategory = productsWithCategory.Where(p => p.CategoryId == categoryId.Value);
            }
            var products = await productsWithCategory.ToListAsync();
            if (products.IsNullOrEmpty())
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsProduct.NOTFOUND, StatusCodes.Status404NotFound);
            }

            var productDtos = new List<ProductResponseDto>();
            foreach (var product in products)
            {
                var dto = _mapper.ProductToProductResponseDto(product);
                dto.CategoryName = product.Category?.CategoryName;
                productDtos.Add(dto);
            }

            return productDtos;
        }

        // get product by Id
        public async Task<ProductResponseDto> GetProductById(int id)
        {
            _logger.Information($"Get product by Id");
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsProduct.NOTFOUND, StatusCodes.Status404NotFound);
            }
            var category = await _categoryRepository.GetByIdAsync(product.CategoryId ?? 0);
            var productDto = _mapper.ProductToProductResponseDto(product);
            productDto.CategoryName = category?.CategoryName;
            return productDto;
        }

        // create product
        public async Task CreateProduct(ProductCreateRequestDto productRequest, CancellationToken cancellationToken = default)
        {
            _logger.Information("Creating new product");

            var existProductName = await _productRepository.GetSingleAsync(x => x.ProductName == productRequest.ProductName);
            if (existProductName != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsProduct.EXISTED_PRODUCTNAME, StatusCodes.Status400BadRequest);
            }
            var existProductImage = await _productRepository.GetSingleAsync(x => x.Image == productRequest.Image);
            if (existProductImage != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsProduct.EXISTED_IMAGE, StatusCodes.Status400BadRequest);
            }
            var existCategory = await _categoryRepository.GetSingleAsync(x => x.Id == productRequest.CategoryId);
            if (existCategory == null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsProduct.NONEXISTENT_CATEGORY, StatusCodes.Status400BadRequest);
            }
            try
            {
                var newProduct = new Product
                {
                    ProductName = productRequest.ProductName,
                    CategoryId = productRequest.CategoryId,
                    Size = productRequest.Size,
                    LastUpdatedTime = DateTimeOffset.UtcNow,
                    CreatedTime = DateTimeOffset.UtcNow,
                    Price = productRequest.Price,
                    RentPrice = productRequest.RentPrice,
                    Description = productRequest.Description,
                    Image = productRequest.Image,
                    Mass = productRequest.Mass,
                    Long = productRequest.Long,
                    Width = productRequest.Width,
                    Height = productRequest.Height,
                };
                _mapper.ProductToCreateProduct(productRequest, newProduct);

                await _productRepository.AddAsync(newProduct, cancellationToken);
                await _unitOfWork.SaveChangeAsync();
                _logger.Information("New product created successfully");
            }
            catch (Exception ex)
            {
                throw new AppException(ResponseCodeConstants.FAILED, ex.Message, StatusCodes.Status400BadRequest);
            }
        }

        // update product

        public async Task UpdateProductAsync(ProductCreateRequestDto productRequest, int id)
        {
            _logger.Information("Update product");
            var existingProduct = await _productRepository.GetSingleAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsProduct.NOTFOUND, StatusCodes.Status404NotFound);
            }

            var productWithSameName = await _productRepository.GetSingleAsync(x => x.ProductName == productRequest.ProductName && x.Id != id);
            if (productWithSameName != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsProduct.EXISTED_PRODUCTNAME, StatusCodes.Status400BadRequest);
            }

            var productWithSameImage = await _productRepository.GetSingleAsync(x => x.Image == productRequest.Image && x.Id != id);
            if (productWithSameImage != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsProduct.EXISTED_IMAGE, StatusCodes.Status400BadRequest);
            }

            var existingCategory = await _categoryRepository.GetSingleAsync(x => x.Id == productRequest.CategoryId);
            if (existingCategory == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsProduct.NONEXISTENT_CATEGORY, StatusCodes.Status400BadRequest);
            }

            if (existingProduct.ProductName == productRequest.ProductName && existingProduct.Image == existingProduct.Image)
            {

                throw new AppException(ResponseCodeConstants.NOTHINGCHANGED, ResponseMessageConstrantsProduct.NOTHING_CHANGED, StatusCodes.Status400BadRequest);
            }

            try
            {
                existingProduct.ProductName = productRequest.ProductName;
                existingProduct.CategoryId = productRequest.CategoryId;
                existingProduct.Size = productRequest.Size;
                existingProduct.LastUpdatedTime = DateTimeOffset.UtcNow;
                existingProduct.Price = productRequest.Price;
                existingProduct.RentPrice = productRequest.RentPrice;
                existingProduct.Description = productRequest.Description;
                existingProduct.Image = productRequest.Image;
                existingProduct.Mass = productRequest.Mass;
                existingProduct.Long = productRequest.Long;
                existingProduct.Width = productRequest.Width;
                existingProduct.Height = productRequest.Height;

                _mapper.ProductToCreateProduct(productRequest, existingProduct);
                _productRepository.Update(existingProduct);
                await _unitOfWork.SaveChangeAsync();
                _logger.Information("Updated product successfully");
            }
            catch (Exception ex)
            {
                throw new AppException(ResponseCodeConstants.FAILED, ex.Message, StatusCodes.Status400BadRequest);
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            _logger.Information($"Update product with id {id}");
            var existProduct = await _productRepository.GetSingleAsync(x => x.Id == id);
            if (existProduct == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsProduct.NOTFOUND, StatusCodes.Status404NotFound);
            }
            _productRepository.Delete(existProduct);
            await _unitOfWork.SaveChangeAsync();
            _logger.Information("Deleted product successfully");
        }
    }
}

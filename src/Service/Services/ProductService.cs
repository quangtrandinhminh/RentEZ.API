using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Microsoft.IdentityModel.Tokens;
using Repository.Infrastructure;
using Repository.Interfaces;
using Service.Interfaces;
using Utility.Constants;
using Utility.Exceptions;
using Service.Mapper;
using Repository.Models;
using System.Security.Claims;
using Service.ApiModels.Product;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Service.Helpers;
using Service.ApiModels.PaginatedList;

namespace Service.Services
{
    public class ProductService(IServiceProvider serviceProvider) : IProductService
    {
        private readonly IProductRepository _productRepository = serviceProvider.GetRequiredService<IProductRepository>();
        private readonly MapperlyMapper _mapper = serviceProvider.GetRequiredService<MapperlyMapper>();
        private readonly ILogger _logger = serviceProvider.GetRequiredService<ILogger>();
        private readonly IUnitOfWork _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        private readonly ICategoryRepository _categoryRepository = serviceProvider.GetRequiredService<ICategoryRepository>();
        private readonly IShopRepository _shopRepository = serviceProvider.GetRequiredService<IShopRepository>();
        private readonly IHttpContextAccessor _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

        // get all products
        public async Task<PaginatedListResponse<ProductListResponse>> GetAllProducts(int pageNumber, int pageSize, int? categoryId = null, int? shopId = null)
        {
            _logger.Information($"Get all products");
            Expression<Func<Product, bool>> predicate = v => v.DeletedTime == null;
            if (categoryId.HasValue)
            {
                var categoryExists = await _categoryRepository.GetSingleAsync(c => c.Id == categoryId.Value);
                if (categoryExists == null)
                {
                    throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsProduct.NONEXISTENT_CATEGORY, StatusCodes.Status404NotFound);
                }
                predicate = predicate.And(p => p.CategoryId == categoryId.Value);
            }

            if (shopId.HasValue)
            {
                var shopExists = await _shopRepository.GetSingleAsync(s => s.Id == shopId.Value);
                if (shopExists == null)
                {
                    throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsShop.NOTFOUND, StatusCodes.Status404NotFound);
                }
                predicate = predicate.And(p => p.ShopId == shopId.Value);
            }

            var products = await _productRepository.GetAllPaginatedQueryable(
                pageNumber,
                pageSize,
                predicate,
                p => p.Id,
                p => p.Category,
                p => p.Shop
            );

            if (products.Items == null || !products.Items.Any())
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsProduct.NOTFOUND, StatusCodes.Status404NotFound);
            }

            var productDtos = _mapper.ProductsToProductsResponseDto(products.Items.ToList());
            foreach (var dto in productDtos)
            {
                var product = products.Items.FirstOrDefault(p => p.Id == dto.Id);

                if (product != null)
                {
                    dto.CategoryName = product.Category?.CategoryName;
                    dto.ShopName = product.Shop?.ShopName;
                    if (!string.IsNullOrEmpty(product.RentPrice))
                    {
                        try
                        {
                            dto.RentPrices = JsonConvert.DeserializeObject<List<double>>(product.RentPrice);
                        }
                        catch (JsonException)
                        {
                            dto.RentPrices = new List<double>();
                            _logger.Warning("Failed to deserialize RentPrice for product ID {ProductId}", product.Id);
                        }
                    }
                }
            }
            return new PaginatedListResponse<ProductListResponse>
            {
                Items = (IReadOnlyCollection<ProductListResponse>)productDtos,
                PageNumber = products.PageNumber,
                TotalPages = products.TotalPages,
                TotalCount = products.TotalCount
            };
        }

        // get product by Id
        public async Task<ProductResponse> GetProductById(int id)
        {
            _logger.Information($"Get product by Id");
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsProduct.NOTFOUND, StatusCodes.Status404NotFound);
            }
            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            var shop = await _shopRepository.GetByIdAsync(product.ShopId);
            var productDto = _mapper.ProductToProductResponseDto(product);
            productDto.CategoryName = category?.CategoryName;
            productDto.ShopName = shop?.ShopName;
            productDto.ShopEmail = shop?.ShopEmail;
            productDto.ShopPhone = shop?.ShopPhone;
            productDto.ShopAddress = shop?.ShopAddress;
            productDto.ShopAvatar = shop?.ShopAvatar;
            if (!string.IsNullOrEmpty(product.RentPrice))
            {
                try
                {
                    productDto.RentPrices = JsonConvert.DeserializeObject<List<double>>(product.RentPrice);
                }
                catch (JsonException)
                {
                    productDto.RentPrices = new List<double>();
                    _logger.Warning($"Failed to deserialize RentPrice for product {product.Id}");
                }
            }
            return productDto;
        }

        // create product
        public async Task CreateProduct(ProductCreateRequest productRequest, CancellationToken cancellationToken = default)
        {
            _logger.Information("Creating new product");
            var currentOwnerIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid);
            if (currentOwnerIdClaim == null)
            {
                throw new AppException(ResponseCodeConstants.UNAUTHORIZED, ResponseMessageConstrantsProduct.UNAUTHORIZED, StatusCodes.Status401Unauthorized);
            }
            var loggedInOwnerId = int.Parse(currentOwnerIdClaim.Value);
            var existShop = await _shopRepository.GetSingleAsync(s => s.OwnerId == loggedInOwnerId);
            if (existShop == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsProduct.SHOP_NOTFOUND, StatusCodes.Status404NotFound);
            }
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
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsProduct.NONEXISTENT_CATEGORY, StatusCodes.Status404NotFound);
            }
            
            var validAllowRentBeforeDays = await _productRepository.GetSingleAsync(x => x.AllowRentBeforeDays == productRequest.AllowRentBeforeDays);
            if (validAllowRentBeforeDays != null && validAllowRentBeforeDays.AllowRentBeforeDays <= 2 && validAllowRentBeforeDays.AllowRentBeforeDays >= 5)
            {
                throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsProduct.INVALID_ALLOWRENTBEFOREDAYS, StatusCodes.Status400BadRequest);
            }
            if (productRequest.RentPrices == null || productRequest.RentPrices.Count != 7)
            {
                throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsProduct.RENTPRICE_EXACT_VALUE, StatusCodes.Status400BadRequest);
            }
            string rentPricesJson = JsonConvert.SerializeObject(productRequest.RentPrices);
            List<double> rentPrices = JsonConvert.DeserializeObject<List<double>>(rentPricesJson);

            //each RentPrice is not more than 30% of the Price
            foreach (var rentPrice in rentPrices)
            {
                if (rentPrice > productRequest.Price * 0.30)
                {
                    throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsProduct.RENTPRICE_INVALID, StatusCodes.Status400BadRequest);
                }
            }
            for (int i = 1; i < rentPrices.Count; i++)
            {
                if (rentPrices[i] <= rentPrices[i - 1])
                {
                    throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsProduct.RENTPRICE_NOT_ASCENDING, StatusCodes.Status400BadRequest);
                }
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
                    ShopId = existShop.Id,
                    Price = productRequest.Price,
                    Quantity = productRequest.Quantity,
                    AllowRentBeforeDays = productRequest.AllowRentBeforeDays,
                    Construction = productRequest.Construction,
                    RentPrice = rentPricesJson,
                    DepositRate = productRequest.DepositRate,
                    Description = productRequest.Description,
                    Image = productRequest.Image,
                    Mass = productRequest.Mass,
                    Long = productRequest.Long,
                    Width = productRequest.Width,
                    Height = productRequest.Height,
                    CreatedBy = loggedInOwnerId
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
        public async Task UpdateProductAsync(ProductCreateRequest productRequest, int id)
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

            var validAllowRentBeforeDays = await _productRepository.GetSingleAsync(x => x.AllowRentBeforeDays == productRequest.AllowRentBeforeDays);
            if (validAllowRentBeforeDays != null && validAllowRentBeforeDays.AllowRentBeforeDays <= 2 && validAllowRentBeforeDays.AllowRentBeforeDays >= 5)
            {
                throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsProduct.INVALID_ALLOWRENTBEFOREDAYS, StatusCodes.Status400BadRequest);
            }

            if (productRequest.RentPrices == null || productRequest.RentPrices.Count != 7)
            {
                throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsProduct.RENTPRICE_EXACT_VALUE, StatusCodes.Status400BadRequest);
            }
            string rentPricesJson = JsonConvert.SerializeObject(productRequest.RentPrices);
            List<double> rentPrices = JsonConvert.DeserializeObject<List<double>>(rentPricesJson);
            // Validate each RentPrice is not more than 30% of the Price
            foreach (var rentPrice in rentPrices)
            {
                if (rentPrice > productRequest.Price * 0.30)
                {
                    throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsProduct.RENTPRICE_INVALID, StatusCodes.Status400BadRequest);
                }
            }

            if (existingProduct.ProductName == productRequest.ProductName && existingProduct.Image == existingProduct.Image && existingProduct.RentPrice == rentPricesJson && existingProduct.Id == id)
            {

                throw new AppException(ResponseCodeConstants.NOTHINGCHANGED, ResponseMessageConstrantsProduct.NOTHING_CHANGED, StatusCodes.Status400BadRequest);
            }

            for (int i = 1; i < rentPrices.Count; i++)
            {
                if (rentPrices[i] <= rentPrices[i - 1])
                {
                    throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsProduct.RENTPRICE_NOT_ASCENDING, StatusCodes.Status400BadRequest);
                }
            }

            try
            {
                existingProduct.ProductName = productRequest.ProductName;
                existingProduct.CategoryId = productRequest.CategoryId;
                existingProduct.Size = productRequest.Size;
                existingProduct.LastUpdatedTime = DateTimeOffset.UtcNow;
                existingProduct.Price = productRequest.Price;
                existingProduct.Quantity = productRequest.Quantity;
                existingProduct.AllowRentBeforeDays = productRequest.AllowRentBeforeDays;
                existingProduct.Construction = productRequest.Construction;
                existingProduct.RentPrice = rentPricesJson;
                existingProduct.DepositRate = productRequest.DepositRate;
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

        public async Task<List<ProductListResponse>> GetOwnProductAsync()
        {
            _logger.Information("Get own products");
            var currentOwnerIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid);
            if (currentOwnerIdClaim == null)
            {
                throw new AppException(ResponseCodeConstants.UNAUTHORIZED, ResponseMessageConstrantsProduct.UNAUTHORIZED, StatusCodes.Status401Unauthorized);
            }
            var loggedInOwnerId = int.Parse(currentOwnerIdClaim.Value);
            var existShop = await _shopRepository.GetSingleAsync(s => s.OwnerId == loggedInOwnerId);
            if (existShop == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsProduct.SHOP_NOTFOUND, StatusCodes.Status404NotFound);
            }
            var listProduct = _productRepository.GetAllWithCondition();
            listProduct = listProduct.Include(x => x.Shop).ThenInclude(x => x.Owner).Where(x => x.ShopId == existShop.Id && x.Shop.OwnerId == loggedInOwnerId);
            if (listProduct == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsProduct.NOTFOUND, StatusCodes.Status404NotFound);
            }
            var products = listProduct.ToList();
            var productDtos = _mapper.ProductsToProductsResponseDto(products);
            return (List<ProductListResponse>)productDtos;
        }
    }
}

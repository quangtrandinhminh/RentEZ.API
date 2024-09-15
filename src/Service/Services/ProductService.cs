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

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly MapperlyMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, MapperlyMapper mapper, ILogger logger, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        // get all products
        public async Task<List<ProductResponseDto>> GetAllProducts()
        {
            _logger.Information($"Get all products");
            var products = await _productRepository.GetAllWithCondition().ToListAsync();
            if (products.IsNullOrEmpty())
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsProduct.NOTFOUND, StatusCodes.Status404NotFound);
            }
            return _mapper.ProductsToProductsResponseDto(products).ToList();
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
            return _mapper.ProductToProductResponseDto(product);
        }
    }
}

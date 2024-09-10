using BusinessObject.DTO.Product;
using BusinessObject.Entities.Product;
using BusinessObject.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Serilog;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;
using Utility.Exceptions;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly MapperlyMapper _mapper;

        public ProductService(IProductRepository productRepository, MapperlyMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        // get all products
        public async Task<List<ProductResponseDto>> GetAllProducts()
        {
            var products = await _productRepository.GetAll();
            return _mapper.ProductsToProductsResponseDto(products).ToList();
        }

        // get product by Id
        public async Task<ProductResponseDto> GetProductById(int id)
        {
            var product = await _productRepository.GetById(id);
            return _mapper.ProductToProductResponseDto(product);
        }
    }
}

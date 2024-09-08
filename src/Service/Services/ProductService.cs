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

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        // get all products
        public async Task<List<ProductEntity>> GetAllProducts()
        {
            return await _productRepository.GetAll();
        }

        // get product by Id
        public async Task<ProductEntity> GetProductById(int id)
        {
            return await _productRepository.GetById(id);
        }
    }
}

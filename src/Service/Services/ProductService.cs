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
    public class ProductService(IServiceProvider serviceProvider) : IProductService
    {
        private readonly IProductRepository _productRepository = serviceProvider.GetRequiredService<IProductRepository>();
        private readonly MapperlyMapper _mapper = serviceProvider.GetRequiredService<MapperlyMapper>();
        private readonly ILogger _logger = Log.Logger;

        // Get all products
        public async Task GetAllProducts()
        {
            _logger.Information("Get all products");
            var products = _productRepository.GetAll();
            if (products is null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstantsUser.USER_NOT_FOUND, StatusCodes.Status404NotFound);
            }
            //var productResponse = _mapper.Map(products);
        }
    }
}

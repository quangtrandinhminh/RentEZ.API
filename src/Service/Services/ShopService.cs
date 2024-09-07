using BusinessObject.DTO.Shop;
using BusinessObject.Entities.Shop;
using BusinessObject.Mapper;
using Microsoft.Extensions.DependencyInjection;
using Repository.Base;
using Repository.Interfaces;
using Serilog;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly MapperlyMapper _mapper;
        private readonly ILogger _logger;

        public ShopService(IServiceProvider serviceProvider)
        {
            _shopRepository = serviceProvider.GetRequiredService<IShopRepository>();
            _mapper = serviceProvider.GetRequiredService<MapperlyMapper>();
            _logger = Log.Logger;
        }

        // create new shop
        public async Task CreateShop(ShopCreateRequestDto shop)
        {
            _logger.Information("Create new shop");

            ShopEntity newShop = new ShopEntity
            {
                ShopEmail = shop.ShopEmail,
                ShopName = shop.ShopName,
                Address = shop.Address,
                Owner_Avatar = shop.Owner_Avatar,
                CreatedTime = DateTime.Now,
            };
            await _shopRepository.CreateShop(newShop);
            await _shopRepository.SaveChangeAsync();
            _logger.Information("New shop created successfully");
        }
    }
}

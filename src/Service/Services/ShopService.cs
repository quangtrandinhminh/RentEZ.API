using BusinessObject.DTO.Shop;
using BusinessObject.Entities.Shop;
using BusinessObject.Mapper;
using Microsoft.EntityFrameworkCore;
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

        public ShopService(IShopRepository shopRepository, MapperlyMapper mapper, ILogger logger)
        {
            _shopRepository = shopRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // create new shop
        public async Task CreateShop(ShopCreateRequestDto shopRequest)
        {
            _logger.Information("Creating new shop");
            var newShop = new ShopEntity();
            _mapper.ShopToCreateShop(shopRequest, newShop);

            newShop.Status = false;
            newShop.CreatedTime = DateTimeOffset.UtcNow;
            newShop.LastUpdatedTime = DateTimeOffset.UtcNow;

            await _shopRepository.CreateAsync(newShop);
            await _shopRepository.SaveChangeAsync();
            _logger.Information("New shop created successfully");
        }

        // get all shops
        public async Task<List<ShopResponseDto>> GetAllShops()
        {
            var shops = await _shopRepository.GetListAsync();
            return _mapper.ShopToShopResponseDto(shops).ToList();
        }
    }
}

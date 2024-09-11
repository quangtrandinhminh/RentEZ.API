using BusinessObject.DTO.Shop;
using BusinessObject.Mapper;
using Repository.Interfaces;
using Serilog;
using Service.Interfaces;
using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;

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
        public async Task CreateShop(ShopCreateRequestDto shopRequest, CancellationToken cancellationToken = default)
        {
            _logger.Information("Creating new shop");
            var newShop = new Shop();
            _mapper.ShopToCreateShop(shopRequest, newShop);

            newShop.CreatedTime = DateTimeOffset.UtcNow;
            newShop.LastUpdatedTime = DateTimeOffset.UtcNow;

            await _shopRepository.AddAsync(newShop, cancellationToken);
            _logger.Information("New shop created successfully");
        }

        // get all shops
        public async Task<List<ShopResponseDto>> GetAllShops()
        {
            var shops = await _shopRepository.GetAllWithCondition(s => s.IsActive && s.DeletedTime == null).ToListAsync();
            return _mapper.ShopToShopResponseDto(shops).ToList();
        }
    }
}

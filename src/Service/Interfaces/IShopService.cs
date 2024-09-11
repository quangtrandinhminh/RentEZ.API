using BusinessObject.DTO.Shop;

namespace Service.Interfaces
{
    public interface IShopService
    {
        Task CreateShop(ShopCreateRequestDto shopRequest, CancellationToken cancellationToken = default);
        Task<List<ShopResponseDto>> GetAllShops();
    }
}

using BusinessObject.DTO.Shop;

namespace Service.Interfaces
{
    public interface IShopService
    {
        Task CreateShop(ShopCreateRequestDto shopRequest, CancellationToken cancellationToken = default);
        Task<List<ShopResponseDto>> GetPendingShopList();
        Task<List<ShopResponseDto>> GetAllShops();
        Task ShopToApprove(int id);
    }
}

using Service.ApiModels.Shop;

namespace Service.Interfaces
{
    public interface IShopService
    {
        Task CreateShop(ShopCreateRequest shopRequest, CancellationToken cancellationToken = default);
        Task<ShopResponse> GetShopById(int id);
        Task<List<ShopResponse>> GetPendingShopList();
        Task<List<ShopResponse>> GetAllShops();
        Task ShopToApprove(int id);
    }
}

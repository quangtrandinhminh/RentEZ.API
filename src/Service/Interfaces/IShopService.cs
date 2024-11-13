using Microsoft.AspNetCore.Mvc;
using Service.ApiModels.Shop;

namespace Service.Interfaces
{
    public interface IShopService
    {
        Task CreateShop(ShopCreateRequest shopRequest, CancellationToken cancellationToken = default);
        Task<ShopResponse> GetShopById(int id);
        Task<List<ShopResponse>> GetPendingShopList();
        Task<List<ShopResponse>> GetAllShops([FromQuery] ShopQueryParams queryParams);
        Task ShopToApprove(int id);
    }
}

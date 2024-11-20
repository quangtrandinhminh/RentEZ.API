using Microsoft.AspNetCore.Mvc;
using Service.ApiModels.PaginatedList;
using Service.ApiModels.Shop;

namespace Service.Interfaces
{
    public interface IShopService
    {
        Task CreateShop(ShopCreateRequest shopRequest, CancellationToken cancellationToken = default);
        Task<ShopResponse> GetShopById(int id);
        Task<List<ShopResponse>> GetPendingShopList();
        Task<PaginatedListResponse<ShopResponse>> GetAllShops([FromQuery] ShopQueryParams queryParams, int pageNumber, int pageSize);
        Task ShopToApprove(int id);
    }
}

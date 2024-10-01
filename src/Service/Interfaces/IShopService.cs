using Service.Models.Shop;
using System.Security.Claims;

namespace Service.Interfaces
{
    public interface IShopService
    {
        Task CreateShop(ShopCreateRequest shopRequest, CancellationToken cancellationToken = default);
        Task<List<ShopResponse>> GetPendingShopList();
        Task<List<ShopResponse>> GetAllShops();
        Task ShopToApprove(int id);
    }
}

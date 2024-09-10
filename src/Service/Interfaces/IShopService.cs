using BusinessObject.DTO.Shop;
using BusinessObject.Entities.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IShopService
    {
        Task CreateShop(ShopCreateRequestDto shopRequest);
        Task<List<ShopResponseDto>> GetAllShops();
    }
}

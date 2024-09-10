using BusinessObject.DTO.Shop;
using BusinessObject.Entities.Identity;
using BusinessObject.Entities.Shop;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IShopRepository
    {
        Task<int> SaveChangeAsync();
        Task<ShopEntity> CreateAsync(ShopEntity shop);
        Task<ShopEntity> GetShopByIdAsync(int shopId);
        Task<List<ShopEntity>> GetListAsync();
        Task<ShopEntity> GetShopByOwnerIdAsync(int ownerId);
    }
}
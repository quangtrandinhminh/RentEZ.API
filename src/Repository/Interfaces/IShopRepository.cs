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
        Task<IdentityResult> CreateShop(ShopEntity shop, CancellationToken cancellationToken = default);
    }
}

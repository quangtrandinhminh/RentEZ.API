using BusinessObject.DTO.User;
using BusinessObject.Entities.Identity;
using BusinessObject.Entities.Shop;
using Microsoft.AspNetCore.Identity;
using Repository.Base;
using Repository.Infrastructure;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ShopRepository : BaseRepository<ShopEntity>, IShopRepository
    {
        private readonly AppDbContext _context = new();

        public ShopRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> SaveChangeAsync() => await _context.SaveChangesAsync();

        public async Task<IdentityResult> CreateShop(ShopEntity shop, CancellationToken cancellationToken = default)
        {
            await _context.Shops.AddAsync(shop, cancellationToken);
            return IdentityResult.Success;
        }
    }
}

using BusinessObject.DTO.Shop;
using BusinessObject.DTO.User;
using BusinessObject.Entities.Identity;
using BusinessObject.Entities.Shop;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Infrastructure;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;

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

        public async Task<ShopEntity> CreateAsync(ShopEntity shop)
        {
            var entity = await _context.Shops.AddAsync(shop);
            await _context.SaveChangesAsync(); 
            return entity.Entity;
        }

        public async Task<ShopEntity> GetShopByIdAsync(int shopId)
        {
            return await _context.Shops.FindAsync(shopId);
        }

        public async Task<List<ShopEntity>> GetListAsync()
        {
            var shops = await GetAllAsync();
            var shopDtos = shops.Select(s => new ShopEntity
            {
                ShopEmail = s.ShopEmail,
                ShopName = s.ShopName,
                Shop_Address = s.Shop_Address,
                Shop_Phone = s.Shop_Phone,
                Shop_Avatar = s.Shop_Avatar,
            }).ToList();
            return shopDtos;
        }

        public async Task<ShopEntity> GetShopByOwnerIdAsync(int ownerId)
        {
            return await _context.Shops.FirstOrDefaultAsync(s => s.OwnerId == ownerId);
        }
    }
}
    
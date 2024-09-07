using BusinessObject.Entities.Category;
using BusinessObject.Entities.Identity;
using BusinessObject.Entities.Product;
using BusinessObject.Entities.Shop;
using Microsoft.EntityFrameworkCore;

namespace Repository.Infrastructure;

public sealed partial class AppDbContext
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserRoleEntity> UserRoleEntity { get; set; }
    public DbSet<RoleEntity> Role { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ShopEntity> Shops {  get; set; }
}
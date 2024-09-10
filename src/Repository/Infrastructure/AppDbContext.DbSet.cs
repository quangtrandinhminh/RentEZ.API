using BusinessObject.Entities;
using BusinessObject.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Repository.Infrastructure;

public sealed partial class AppDbContext
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserRoleEntity> UserRoleEntity { get; set; }
    public DbSet<RoleEntity> Role { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Shop> Shops {  get; set; }
    public DbSet<Category> Categories { get; set; }
}
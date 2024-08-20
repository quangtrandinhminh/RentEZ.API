using BusinessObject.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Repository.Infrastructure;

public sealed partial class AppDbContext
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserRoleEntity> UserRoleEntity { get; set; }
    public DbSet<RoleEntity> Role { get; set; }
}
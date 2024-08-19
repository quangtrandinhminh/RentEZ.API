using BusinessObject;
using BusinessObject.Entities;
using BusinessObject.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Utility.Enum;

namespace Repository;

public class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, int>
{
    public const int CommandTimeoutInSecond = 20 * 60;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.SetCommandTimeout(CommandTimeoutInSecond);
    }

    public AppDbContext()
    {

    }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserRoleEntity> UserRoleEntity { get; set; }
    public DbSet<RoleEntity> Role { get; set; }

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];
        return strConn;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>(b =>
        {
            // Each User can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        modelBuilder.Entity<RoleEntity>(b =>
        {
            // Each Role can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        });

        var admin = new UserEntity
        {
            Id = 1,
            UserName = "admin",
            FullName = "Admin User",
            NormalizedUserName = "ADMIN",
            Email = "admin@email.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        modelBuilder.Entity<UserEntity>().HasData(admin);

        var cus1 = new UserEntity
        {
            Id = 2,
            UserName = "customer1",
            FullName = "Customer 1",
            NormalizedUserName = "CUSTOMER1",
            Email = ""
        };
        modelBuilder.Entity<UserEntity>().HasData(cus1);

        var roles = new List<RoleEntity>
        {
            new()
            {
                Id = 1,
                Name = UserRole.Admin.ToString(),
                NormalizedName = UserRole.Admin.ToString().ToUpper()
            },
            new()
            {
                Id = 4,
                Name = UserRole.Customer.ToString(),
                NormalizedName = UserRole.Customer.ToString().ToUpper()
            },
        };
        modelBuilder.Entity<RoleEntity>().HasData(roles);

        var adminUserRole = new UserRoleEntity
        {
            UserId = admin.Id,
            RoleId = 1
        };
        modelBuilder.Entity<UserRoleEntity>().HasData(adminUserRole);

        var cus1UserRole = new UserRoleEntity
        {
            UserId = cus1.Id,
            RoleId = 4
        };
        modelBuilder.Entity<UserRoleEntity>().HasData(cus1UserRole);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
    }
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Models;
using Repository.Models.Identity;
using Utility.Enum;
using Utility.Helpers;

namespace Repository.Infrastructure;

public sealed partial class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, int>
{
    public const int CommandTimeoutInSecond = 20 * 60;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.SetCommandTimeout(CommandTimeoutInSecond);
    }

    public AppDbContext()
    {

    }

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        //var strConn = config["ConnectionStrings:DefaultConnection"];
        var strConn = config["ConnectionStrings:PostgresConnection"];
        return strConn;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer(GetConnectionString());
        => optionsBuilder.UseNpgsql(GetConnectionString());

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

        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.ManagedShop) 
            .WithOne(s => s.Owner)
            .HasForeignKey<Shop>(s => s.OwnerId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder.Entity<Category>(c =>
        {
            c.HasMany(p => p.ProductEntities)
             .WithOne(c => c.Category)
             .HasForeignKey(ci => ci.CategoryId)
             .IsRequired();
        });

        modelBuilder.Entity<Shop>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();

        // create index
        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.Email);

        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.PhoneNumber);

        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.CreatedTime);


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
            Email = "admin@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
            SecurityStamp = Guid.NewGuid().ToString(),
            Verified = CoreHelper.SystemTimeNow,
        };
        modelBuilder.Entity<UserEntity>().HasData(admin);

        var shopOwner = new UserEntity
        {
            Id = 2,
            UserName = "shopowner",
            FullName = "Shop Owner",
            NormalizedUserName = "SHOPOWNER",
            Email = "shopowner@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
            SecurityStamp = Guid.NewGuid().ToString(),
            Verified = CoreHelper.SystemTimeNow,
        };
        modelBuilder.Entity<UserEntity>().HasData(shopOwner);

        var cus1 = new UserEntity
        {
            Id = 3,
            UserName = "customer1",
            FullName = "Customer 1",
            NormalizedUserName = "CUSTOMER1",
            Email = "customer1@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
            SecurityStamp = Guid.NewGuid().ToString(),
            Verified = CoreHelper.SystemTimeNow,
        };
        modelBuilder.Entity<UserEntity>().HasData(cus1);

        int roleIndex = 1;
        var roles = Enum.GetValues<UserRole>().Select(role => new RoleEntity
        {
            Id = roleIndex++,
            Name = role.ToString(),
            NormalizedName = role.ToString().ToUpper()
        }).ToArray();
        modelBuilder.Entity<RoleEntity>().HasData(roles);

        var adminUserRole = new UserRoleEntity
        {
            UserId = admin.Id,
            RoleId = 1
        };
        modelBuilder.Entity<UserRoleEntity>().HasData(adminUserRole);

        var shopOwnerUserRole = new UserRoleEntity
        {
            UserId = shopOwner.Id,
            RoleId = 2
        };

        var cus1UserRole = new UserRoleEntity
        {
            UserId = cus1.Id,
            RoleId = 3
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
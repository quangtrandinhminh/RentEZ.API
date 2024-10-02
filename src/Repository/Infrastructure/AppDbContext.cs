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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

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

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }

        int roleIndex = 1;
        var roles = Enum.GetValues<UserRoleEnum>().Select(role => new RoleEntity
        {
            Id = roleIndex++,
            Name = role.ToString(),
            NormalizedName = role.ToString().ToUpper()
        }).ToArray();

        // Seed data for roles
        modelBuilder.Entity<RoleEntity>().HasData(roles);

        // Seed data for users
        modelBuilder.Entity<UserEntity>().HasData(
            new UserEntity
            {
                Id = 1,
                UserName = "admin",
                FullName = "Admin User",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
                SecurityStamp = Guid.NewGuid().ToString(),
                Verified = CoreHelper.SystemTimeNow,
                Avatar = "https://via.placeholder.com/150",
            },
            new UserEntity
            {
                Id = 2,
                UserName = "shopowner",
                FullName = "Shop Owner",
                NormalizedUserName = "SHOPOWNER",
                Email = "shopowner@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
                SecurityStamp = Guid.NewGuid().ToString(),
                Verified = CoreHelper.SystemTimeNow,
                Avatar = "https://via.placeholder.com/150",
            },
            new UserEntity
            {
                Id = 3,
                UserName = "customer1",
                FullName = "Customer 1",
                NormalizedUserName = "CUSTOMER1",
                Email = "customer1@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
                SecurityStamp = Guid.NewGuid().ToString(),
                Verified = CoreHelper.SystemTimeNow,
                Avatar = "https://via.placeholder.com/150",
            }
        );
        modelBuilder.Entity<UserRoleEntity>().HasData(
            new UserRoleEntity { UserId = 1, RoleId = 1 },
            new UserRoleEntity { UserId = 2, RoleId = 2 },
            new UserRoleEntity { UserId = 3, RoleId = 3 }
        );

        // Seed data shop
        modelBuilder.Entity<Shop>().HasData(
            new Shop
            {
                Id = 1,
                ShopEmail = "shopowner@example.com",
                ShopName = "Shop Owner",
                ShopPhone = "0123456789",
                ShopAddress = "123 Street",
                ShopAvatar = "https://via.placeholder.com/150",
                Verified = CoreHelper.SystemTimeNow,
                OwnerId = 2
            });

        // Seed data category from enum
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, CategoryName = CategoryEnum.Handbag.ToString(), Description = "Túi xách" },
            new Category { Id = 2, CategoryName = CategoryEnum.SetOfClothes.ToString(), Description = "Bộ quần áo" },
            new Category { Id = 3, CategoryName = CategoryEnum.Dress.ToString(), Description = "Váy" },
            new Category { Id = 4, CategoryName = CategoryEnum.Shoes.ToString(), Description = "Giày dép" }
         );

        // Seed data product
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                ProductName = "Túi xách 1",
                CategoryId = 1,
                ShopId = 1,
                Size = 10,
                Price = 100000,
                RentPrice = 50000,
                RentedCount = 0,
                RatingCount = 0,
                Description = "Túi xách đẹp",
                Image = "https://via.placeholder.com/150",
                Mass = 1,
                Long = 10,
                Width = 5,
                Height = 15
            },
            new Product
            {
                Id = 2,
                ProductName = "Túi xách 2",
                CategoryId = 1,
                ShopId = 1,
                Size = 10,
                Price = 100000,
                RentPrice = 50000,
                RentedCount = 0,
                RatingCount = 0,
                Description = "Túi xách đẹp",
                Image = "https://via.placeholder.com/150",
                Mass = 1,
                Long = 10,
                Width = 5,
                Height = 15
            },
            new Product
            {
                Id = 3,
                ProductName = "Bộ quần áo 1",
                CategoryId = 2,
                ShopId = 1,
                Size = 10,
                Price = 100000,
                RentPrice = 50000,
                RentedCount = 0,
                RatingCount = 0,
                Description = "Bộ quần áo đẹp",
                Image = "https://via.placeholder.com/150",
                Mass = 1,
                Long = 10,
                Width = 5,
                Height = 15
            },
            new Product
            {
                Id = 4,
                ProductName = "Bộ quần áo 2",
                CategoryId = 2,
                ShopId = 1,
                Size = 10,
                Price = 100000,
                RentPrice = 50000,
                RentedCount = 0,
                RatingCount = 0,
                Description = "Bộ quần áo đẹp",
                Image = "https://via.placeholder.com/150",
                Mass = 1,
                Long = 10,
            }
            );
    }
}
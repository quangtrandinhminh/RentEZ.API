using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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

    /*private string GetConnectionString()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettingsconfig.json", optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();

        return config.GetConnectionString("PostgresConnection");
    }
    //private string GetConnectionString()
    //{
    //    IConfiguration config = new ConfigurationBuilder()
    //        .SetBasePath(Directory.GetCurrentDirectory())
    //        .AddJsonFile("appsettings.json", true, true)
    //        .Build();
    //    //var strConn = config["ConnectionStrings:DefaultConnection"];
    //    var strConn = config["ConnectionStrings:PostgresConnection"];
    //    return strConn;
    //}

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    //=> optionsBuilder.UseSqlServer(GetConnectionString());
    //    => optionsBuilder.UseNpgsql(GetConnectionString());
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(GetConnectionString());
        }
    }*/

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
            new UserRoleEntity { UserId = 2, RoleId = 3 },
            new UserRoleEntity { UserId = 3, RoleId = 3 },
            new UserRoleEntity { UserId = 3, RoleId = 2 }
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
            },
            new Shop
            {
                Id = 2,
                ShopEmail = "customershop1@example.com",
                ShopName = "Customer Shop",
                ShopPhone = "0123456789",
                ShopAddress = "123 Street",
                ShopAvatar = "https://via.placeholder.com/150",
                Verified = CoreHelper.SystemTimeNow,
                OwnerId = 3
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
                RentPrice = JsonConvert.SerializeObject(new List<double> {
                    25000, // 25% của Price
                    27000,
                    28000,
                    29000,
                    29500,
                    29800,
                    30000  // Max 30% của Price
                }),
                RentedCount = 0,
                RatingCount = 0,
                Quantity = 5,
                AllowRentBeforeDays = 3,
                Construction = 5,
                Description = "Túi xách đẹp",
                Image = "https://via.placeholder.com/150",
                Mass = 1,
                Long = 10,
                Width = 5,
                Height = 15,
                DepositRate = 0.5,
                CreatedTime = DateTimeOffset.UtcNow,
                LastUpdatedTime = DateTimeOffset.UtcNow,
            },
            new Product
            {
                Id = 2,
                ProductName = "Túi xách 2",
                CategoryId = 1,
                ShopId = 1,
                Size = 10,
                Price = 100000,
                RentPrice = JsonConvert.SerializeObject(new List<double> {
                    20000, // 20% của Price
                    22000,
                    24000,
                    26000,
                    27000,
                    28000,
                    29000  // 29% của Price
                }),
                RentedCount = 0,
                RatingCount = 0,
                Quantity = 3,
                AllowRentBeforeDays = 4,
                Construction = 5,
                Description = "Túi xách đẹp",
                Image = "https://via.placeholder.com/150",
                Mass = 1,
                Long = 10,
                Width = 5,
                Height = 15,
                DepositRate = 0.5,
                CreatedTime = DateTimeOffset.UtcNow,
                LastUpdatedTime = DateTimeOffset.UtcNow,
            },
            new Product
            {
                Id = 3,
                ProductName = "Bộ quần áo 1",
                CategoryId = 2,
                ShopId = 2,
                Size = 10,
                Price = 100000,
                RentPrice = JsonConvert.SerializeObject(new List<double> {
                    15000, // 15% của Price
                    18000,
                    21000,
                    23000,
                    25000,
                    27000,
                    29000  // 29% của Price
                }),
                RentedCount = 0,
                RatingCount = 0,
                Quantity = 8,
                AllowRentBeforeDays = 3,
                Construction = 5,
                Description = "Bộ quần áo đẹp",
                Image = "https://via.placeholder.com/150",
                Mass = 1,
                Long = 10,
                Width = 5,
                Height = 15,
                DepositRate = 0.7,
                CreatedTime = DateTimeOffset.UtcNow,
                LastUpdatedTime = DateTimeOffset.UtcNow,
            },
            new Product
            {
                Id = 4,
                ProductName = "Bộ quần áo 2",
                CategoryId = 2,
                ShopId = 1,
                Size = 10,
                Price = 100000,
                RentPrice = JsonConvert.SerializeObject(new List<double> {
                    20000, // 20% của Price
                    22000,
                    24000,
                    25000,
                    26000,
                    28000,
                    29500  // 29.5% của Price
                }),
                RentedCount = 0,
                RatingCount = 0,
                Quantity = 4,
                AllowRentBeforeDays = 4,
                Construction = 5,
                Description = "Bộ quần áo đẹp",
                Image = "https://via.placeholder.com/150",
                Mass = 1,
                Long = 10,
                Width = 5,
                Height = 15,
                DepositRate = 0.7,
                CreatedTime = DateTimeOffset.UtcNow,
                LastUpdatedTime = DateTimeOffset.UtcNow,
            }
            );

        // Seed data voucher
        modelBuilder.Entity<Voucher>().HasData(
            new Voucher
            {
                Id = 1,
                ShopId = null,
                Code = "Voucher1",
                Name = "Voucher 100k",
                Value = 100000,
                Type = VoucherTypeEnum.Rent,
                ValueType = VoucherValueTypeEnum.Amount,
                Limit = 10,
                Description = "Giảm 100.000đ phí thuê, RentEZ chào bạn mới"
            },
            new Voucher
            {
                Id = 2,
                ShopId = 1,
                Code = "Voucher2",
                Name = "Voucher 10% phí thuê",
                Value = 0.10m,
                MinRentValue = 250000,
                MaxDiscountValue = 100000,
                Type = VoucherTypeEnum.Rent,
                ValueType = VoucherValueTypeEnum.Percent,
                StartDate = CoreHelper.SystemTimeNow,
                EndDate = CoreHelper.SystemTimeNow.AddDays(10),
                Limit = 10,
                Description = "Giảm 10% phí thuê cho đơn hàng từ Shop 1 có tiền thuê từ 250.000đ tối đa 100.000"
            },
            new Voucher
            {
                Id = 3,
                ShopId = 2,
                Code = "Voucher3",
                Name = "Voucher 10% phí thuê",
                Value = 0.10m,
                Type = VoucherTypeEnum.Rent,
                ValueType = VoucherValueTypeEnum.Percent,
                StartDate = CoreHelper.SystemTimeNow,
                EndDate = CoreHelper.SystemTimeNow.AddDays(10),
                Limit = 10,
                Description = "Giảm 10% phí thuê cho mọi đơn hàng từ Shop 2"
            },
            new Voucher
            {
                Id = 4,
                ShopId = null,
                Code = "Voucher4",
                Name = "Voucher 30% phí ship",
                Value = 0.30m,
                MinRentValue = 100000,
                MaxDiscountValue = 10000,
                Type = VoucherTypeEnum.Ship,
                ValueType = VoucherValueTypeEnum.Percent,
                Description = "Giảm 30% phí ship cho đơn hàng có phí thuê từ 100.000đ, tối đa 10.000đ"
            },
            new Voucher
            {
                Id = 5,
                ShopId = null,
                Code = "Voucher5",
                Name = "Voucher 15% phí ship",
                Value = 0.5m,
                MinRentValue = 300000,
                MaxDiscountValue = 25000,
                Type = VoucherTypeEnum.Ship,
                ValueType = VoucherValueTypeEnum.Percent,
                Description = "Giảm 50% phí ship cho đơn hàng có phí thuê từ 300.000đ, tối đa 25.000đ"
            },
            new Voucher
            {
                Id = 6,
                ShopId = null,
                Code = "Voucher5",
                Name = "Voucher 15% phí ship",
                Value = 0.7m,
                MinRentValue = 150000,
                MaxDiscountValue = 75000,
                Type = VoucherTypeEnum.Ship,
                ValueType = VoucherValueTypeEnum.Percent,
                Description = "Giảm 70% phí ship cho đơn hàng có phí thuê từ 500.000đ, tối đa 75.000đ"
            });
    }
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.Models;
using Repository.Models.Identity;
using Utility.Config;
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

        /*int roleIndex = 1;
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
                UserName = AdminSetting.Instance.Username,
                FullName = "Admin User",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",            
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(AdminSetting.Instance.Password),
                SecurityStamp = Guid.NewGuid().ToString(),
                Verified = CoreHelper.SystemTimeNow,
                Avatar = "https://via.placeholder.com/150",
            }
        );
        modelBuilder.Entity<UserRoleEntity>().HasData(
            new UserRoleEntity { UserId = 1, RoleId = 1 }
        );

        // Seed data category from enum
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, CategoryName = CategoryEnum.Handbag.ToString(), Description = "Túi xách" },
            new Category { Id = 2, CategoryName = CategoryEnum.SetOfClothes.ToString(), Description = "Bộ quần áo" },
            new Category { Id = 3, CategoryName = CategoryEnum.Dress.ToString(), Description = "Váy" },
            new Category { Id = 4, CategoryName = CategoryEnum.Shoes.ToString(), Description = "Giày dép" }
            );*/
    }
}
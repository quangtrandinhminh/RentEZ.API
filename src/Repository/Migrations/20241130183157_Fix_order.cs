using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Fix_order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "PlatformRentPriceDiscount",
                table: "OrderShops");

            migrationBuilder.AlterColumn<decimal>(
                name: "ShipSupportFee",
                table: "Orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ShipFee",
                table: "Orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PlatformRentDiscount",
                table: "Orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProvisionalTotal",
                table: "Orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ShopRentDiscount",
                table: "Orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 18, 31, 57, 52, DateTimeKind.Unspecified).AddTicks(8022), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 18, 31, 57, 52, DateTimeKind.Unspecified).AddTicks(8022), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 18, 31, 57, 52, DateTimeKind.Unspecified).AddTicks(8063), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 18, 31, 57, 52, DateTimeKind.Unspecified).AddTicks(8063), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 18, 31, 57, 52, DateTimeKind.Unspecified).AddTicks(8067), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 18, 31, 57, 52, DateTimeKind.Unspecified).AddTicks(8067), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 18, 31, 57, 52, DateTimeKind.Unspecified).AddTicks(8070), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 18, 31, 57, 52, DateTimeKind.Unspecified).AddTicks(8070), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "1b6f7028-b7a1-46c6-861a-0c752c28bb20", new DateTimeOffset(new DateTime(2024, 11, 30, 18, 31, 56, 927, DateTimeKind.Unspecified).AddTicks(871), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 18, 31, 56, 927, DateTimeKind.Unspecified).AddTicks(871), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$gRLFtLfrh2NMeazyNJUuMO9SOASlpfbQj8RKsCPQj5w5LPRvcdJyS", new DateTimeOffset(new DateTime(2024, 11, 30, 18, 31, 57, 52, DateTimeKind.Unspecified).AddTicks(7371), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlatformRentDiscount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProvisionalTotal",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShopRentDiscount",
                table: "Orders");

            migrationBuilder.AddColumn<decimal>(
                name: "PlatformRentPriceDiscount",
                table: "OrderShops",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "ShipSupportFee",
                table: "Orders",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "ShipFee",
                table: "Orders",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3462), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3462), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3503), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3503), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3505), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3505), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3508), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3508), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "c0d36315-c24a-4854-9f36-1725fb541d06", new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 41, 909, DateTimeKind.Unspecified).AddTicks(9739), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 41, 909, DateTimeKind.Unspecified).AddTicks(9739), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$iPvCZmTOMvt.hVuVq2D0g.U2AVB8GumcValuBTllCOK6yfBtCmufW", new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 41, DateTimeKind.Unspecified).AddTicks(2819), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AccountName", "AccountNo", "Address", "Avatar", "BankId", "BirthDate", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Email", "EmailConfirmed", "FullName", "LastUpdatedBy", "LastUpdatedTime", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OTP", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "UserName", "Verified" },
                values: new object[,]
                {
                    { 2, 0, null, null, null, "https://via.placeholder.com/150", null, null, "366ad63d-4ae6-470b-b6c8-a31b3125f623", null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 41, DateTimeKind.Unspecified).AddTicks(2853), new TimeSpan(0, 0, 0, 0, 0)), null, null, "shopowner@example.com", false, "Shop Owner", null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 41, DateTimeKind.Unspecified).AddTicks(2853), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "SHOPOWNER", null, "$2a$11$wRcubXZE8SZQHp9ZWek5ZeLg0zk357WU16ze/xgoGWY1GbrHyfHnm", null, false, false, "shopowner", new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 179, DateTimeKind.Unspecified).AddTicks(3710), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 3, 0, null, null, null, "https://via.placeholder.com/150", null, null, "ea635b97-e8f2-4d7d-9769-afcfc6d580e5", null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 179, DateTimeKind.Unspecified).AddTicks(3747), new TimeSpan(0, 0, 0, 0, 0)), null, null, "customer1@example.com", false, "Customer 1", null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 179, DateTimeKind.Unspecified).AddTicks(3747), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "CUSTOMER1", null, "$2a$11$IwoObiPR7N2ygE.wuDBXHezFxmBqChQ0L5rWiaV6Pb8yGQYtr/9W.", null, false, false, "customer1", new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(2408), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "EndDate", "LastUpdatedBy", "LastUpdatedTime", "Limit", "MaxDiscountValue", "MinRentValue", "Name", "ShopId", "StartDate", "Type", "Used", "Value", "ValueType" },
                values: new object[,]
                {
                    { 1, "Voucher1", null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4805), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Giảm 100.000đ phí thuê, RentEZ chào bạn mới", null, null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4805), new TimeSpan(0, 0, 0, 0, 0)), 10, null, null, "Voucher 100k", null, null, 1, 0, 100000m, 2 },
                    { 4, "Voucher4", null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4886), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Giảm 30% phí ship cho đơn hàng có phí thuê từ 100.000đ, tối đa 10.000đ", null, null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4886), new TimeSpan(0, 0, 0, 0, 0)), null, 10000m, 100000m, "Voucher 30% phí ship", null, null, 2, 0, 0.30m, 1 },
                    { 5, "Voucher5", null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4889), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Giảm 50% phí ship cho đơn hàng có phí thuê từ 300.000đ, tối đa 25.000đ", null, null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4889), new TimeSpan(0, 0, 0, 0, 0)), null, 25000m, 300000m, "Voucher 15% phí ship", null, null, 2, 0, 0.5m, 1 },
                    { 6, "Voucher5", null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4891), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Giảm 70% phí ship cho đơn hàng có phí thuê từ 500.000đ, tối đa 75.000đ", null, null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4891), new TimeSpan(0, 0, 0, 0, 0)), null, 75000m, 150000m, "Voucher 15% phí ship", null, null, 2, 0, 0.7m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "AccountName", "AccountNo", "BankId", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime", "OwnerId", "ShopAddress", "ShopAvatar", "ShopEmail", "ShopName", "ShopPhone", "Verified" },
                values: new object[,]
                {
                    { 1, null, null, null, null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3382), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3382), new TimeSpan(0, 0, 0, 0, 0)), 2, "123 Street", "https://via.placeholder.com/150", "shopowner@example.com", "Shop Owner", "0123456789", new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3387), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2, null, null, null, null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3389), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3389), new TimeSpan(0, 0, 0, 0, 0)), 3, "123 Street", "https://via.placeholder.com/150", "customershop1@example.com", "Customer Shop", "0123456789", new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3391), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AllowRentBeforeDays", "CategoryId", "Construction", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "DepositRate", "Description", "Height", "Image", "LastUpdatedBy", "LastUpdatedTime", "Long", "Mass", "Price", "ProductName", "Quantity", "RentPrice", "RentedCount", "ShopId", "Size", "Width" },
                values: new object[,]
                {
                    { 1, 3, 1, 5, null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4313), new TimeSpan(0, 0, 0, 0, 0)), null, null, 0.5, "Túi xách đẹp", 15.0, "https://via.placeholder.com/150", null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4313), new TimeSpan(0, 0, 0, 0, 0)), 10.0, 1.0, 100000.0, "Túi xách 1", 5, "[25000.0,27000.0,28000.0,29000.0,29500.0,29800.0,30000.0]", 0, 1, 10.0, 5.0 },
                    { 2, 4, 1, 5, null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4424), new TimeSpan(0, 0, 0, 0, 0)), null, null, 0.5, "Túi xách đẹp", 15.0, "https://via.placeholder.com/150", null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4425), new TimeSpan(0, 0, 0, 0, 0)), 10.0, 1.0, 100000.0, "Túi xách 2", 3, "[20000.0,22000.0,24000.0,26000.0,27000.0,28000.0,29000.0]", 0, 1, 10.0, 5.0 },
                    { 3, 3, 2, 5, null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4505), new TimeSpan(0, 0, 0, 0, 0)), null, null, 0.69999999999999996, "Bộ quần áo đẹp", 15.0, "https://via.placeholder.com/150", null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4506), new TimeSpan(0, 0, 0, 0, 0)), 10.0, 1.0, 100000.0, "Bộ quần áo 1", 8, "[15000.0,18000.0,21000.0,23000.0,25000.0,27000.0,29000.0]", 0, 2, 10.0, 5.0 },
                    { 4, 4, 2, 5, null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4707), new TimeSpan(0, 0, 0, 0, 0)), null, null, 0.69999999999999996, "Bộ quần áo đẹp", 15.0, "https://via.placeholder.com/150", null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4708), new TimeSpan(0, 0, 0, 0, 0)), 10.0, 1.0, 100000.0, "Bộ quần áo 2", 4, "[20000.0,22000.0,24000.0,25000.0,26000.0,28000.0,29500.0]", 0, 1, 10.0, 5.0 }
                });

            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "EndDate", "LastUpdatedBy", "LastUpdatedTime", "Limit", "MaxDiscountValue", "MinRentValue", "Name", "ShopId", "StartDate", "Type", "Used", "Value", "ValueType" },
                values: new object[,]
                {
                    { 2, "Voucher2", null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4819), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Giảm 10% phí thuê cho đơn hàng từ Shop 1 có tiền thuê từ 250.000đ tối đa 100.000", new DateTimeOffset(new DateTime(2024, 12, 6, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4828), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4819), new TimeSpan(0, 0, 0, 0, 0)), 10, 100000m, 250000m, "Voucher 10% phí thuê", 1, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4827), new TimeSpan(0, 0, 0, 0, 0)), 1, 0, 0.10m, 1 },
                    { 3, "Voucher3", null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4876), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Giảm 10% phí thuê cho mọi đơn hàng từ Shop 2", new DateTimeOffset(new DateTime(2024, 12, 6, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4878), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4876), new TimeSpan(0, 0, 0, 0, 0)), 10, null, null, "Voucher 10% phí thuê", 2, new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4878), new TimeSpan(0, 0, 0, 0, 0)), 1, 0, 0.10m, 1 }
                });
        }
    }
}

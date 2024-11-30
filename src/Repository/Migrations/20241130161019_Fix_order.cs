using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Fix_order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(5739), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(5739), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(5782), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(5782), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(5785), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(5785), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(5790), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(5790), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(6802), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(6804), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(6992), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(6992), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7126), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7127), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7344), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7345), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(5622), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(5622), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(5635), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(5639), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(5639), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(5641), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "d7f6bd09-de83-4968-8eef-5aac1e6901fa", new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 18, 845, DateTimeKind.Unspecified).AddTicks(959), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 18, 845, DateTimeKind.Unspecified).AddTicks(959), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$jYmxez.fJV9s6h6xcvRHduT0cUrHe6j1mshagUgv/Sf0A7apOtRzG", new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 18, 993, DateTimeKind.Unspecified).AddTicks(5570), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "c955289e-523a-4c8d-af85-3c54507d0262", new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 18, 993, DateTimeKind.Unspecified).AddTicks(5601), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 18, 993, DateTimeKind.Unspecified).AddTicks(5601), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$yqPAhKW8e/uJFEF9vKOv9.WiKDViyB2fN6h34lyH.AC7ceV4Ztd/q", new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 137, DateTimeKind.Unspecified).AddTicks(7459), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "b24ecd7d-0e39-426e-9fd2-5982b28d748e", new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 137, DateTimeKind.Unspecified).AddTicks(7480), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 137, DateTimeKind.Unspecified).AddTicks(7480), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$cS4H/UP8M7J187YpnQSmJOFxSH1SBLD1dMCRXxTp3jwNMeBGslw46", new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(4205), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7476), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7476), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "EndDate", "LastUpdatedTime", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7492), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 10, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7510), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7492), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7508), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "EndDate", "LastUpdatedTime", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7564), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 10, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7568), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7564), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7567), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7578), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7578), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7586), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7586), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7591), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 30, 16, 10, 19, 278, DateTimeKind.Unspecified).AddTicks(7591), new TimeSpan(0, 0, 0, 0, 0)) });
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
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4313), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4313), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4424), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4425), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4505), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4506), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4707), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4708), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3382), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3382), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3387), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3389), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3389), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(3391), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "c0d36315-c24a-4854-9f36-1725fb541d06", new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 41, 909, DateTimeKind.Unspecified).AddTicks(9739), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 41, 909, DateTimeKind.Unspecified).AddTicks(9739), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$iPvCZmTOMvt.hVuVq2D0g.U2AVB8GumcValuBTllCOK6yfBtCmufW", new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 41, DateTimeKind.Unspecified).AddTicks(2819), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "366ad63d-4ae6-470b-b6c8-a31b3125f623", new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 41, DateTimeKind.Unspecified).AddTicks(2853), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 41, DateTimeKind.Unspecified).AddTicks(2853), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$wRcubXZE8SZQHp9ZWek5ZeLg0zk357WU16ze/xgoGWY1GbrHyfHnm", new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 179, DateTimeKind.Unspecified).AddTicks(3710), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "ea635b97-e8f2-4d7d-9769-afcfc6d580e5", new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 179, DateTimeKind.Unspecified).AddTicks(3747), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 179, DateTimeKind.Unspecified).AddTicks(3747), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$IwoObiPR7N2ygE.wuDBXHezFxmBqChQ0L5rWiaV6Pb8yGQYtr/9W.", new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(2408), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4805), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4805), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "EndDate", "LastUpdatedTime", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4819), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 6, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4828), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4819), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4827), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "EndDate", "LastUpdatedTime", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4876), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 6, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4878), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4876), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4878), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4886), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4886), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4889), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4889), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4891), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 8, 42, 309, DateTimeKind.Unspecified).AddTicks(4891), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProduct_RentPrice_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RentPrice",
                table: "Products",
                type: "text",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2317), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2317), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2334), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2334), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2336), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2336), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2338), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2338), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AllowRentBeforeDays", "Construction", "CreatedTime", "LastUpdatedTime", "Quantity", "RentPrice" },
                values: new object[] { 3, 5, new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2793), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2794), new TimeSpan(0, 0, 0, 0, 0)), 5, "[25000.0,27000.0,28000.0,29000.0,29500.0,29800.0,30000.0]" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AllowRentBeforeDays", "Construction", "CreatedTime", "LastUpdatedTime", "Quantity", "RentPrice" },
                values: new object[] { 4, 5, new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2868), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2869), new TimeSpan(0, 0, 0, 0, 0)), 3, "[20000.0,22000.0,24000.0,26000.0,27000.0,28000.0,29000.0]" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AllowRentBeforeDays", "Construction", "CreatedTime", "LastUpdatedTime", "Quantity", "RentPrice" },
                values: new object[] { 3, 5, new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2991), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2991), new TimeSpan(0, 0, 0, 0, 0)), 8, "[15000.0,18000.0,21000.0,23000.0,25000.0,27000.0,29000.0]" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AllowRentBeforeDays", "Construction", "CreatedTime", "LastUpdatedTime", "Quantity", "RentPrice" },
                values: new object[] { 4, 5, new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3120), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3121), new TimeSpan(0, 0, 0, 0, 0)), 4, "[20000.0,22000.0,24000.0,25000.0,26000.0,28000.0,29500.0]" });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2279), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2279), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2282), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2285), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2285), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2287), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "13c6ada8-4ad2-4af6-9c08-0d10c98e116c", new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 293, DateTimeKind.Unspecified).AddTicks(479), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 293, DateTimeKind.Unspecified).AddTicks(479), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$NXzlYyeeZ0tS8F75T9B9J.iRqQJbdNYUB0XLMowulzOWpP.A.C686", new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 420, DateTimeKind.Unspecified).AddTicks(9134), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "e9e1928f-f7eb-4dd5-b711-a880f1e8434e", new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 420, DateTimeKind.Unspecified).AddTicks(9166), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 420, DateTimeKind.Unspecified).AddTicks(9166), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$IuFptFyZm5cvlOs4KR7jaOQvDiVVBQHgC1NkFO8pJxBwgUT72T5R.", new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 552, DateTimeKind.Unspecified).AddTicks(1003), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "12b5273c-723b-47a8-9a74-d9e94c2d9b1f", new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 552, DateTimeKind.Unspecified).AddTicks(1041), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 552, DateTimeKind.Unspecified).AddTicks(1041), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$.TGpwLagvqWGVMB0t4AW7umxylq4BxtaWmN4.hnbur9ypYi5aIHly", new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2018), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3167), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3167), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "EndDate", "LastUpdatedTime", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3176), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 7, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3186), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3176), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3184), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "EndDate", "LastUpdatedTime", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3204), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 7, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3216), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3204), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3205), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3218), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3218), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3220), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3220), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3223), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3223), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "RentPrice",
                table: "Products",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2976), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2976), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3014), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3014), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3016), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3016), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3024), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3024), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AllowRentBeforeDays", "Construction", "CreatedTime", "LastUpdatedTime", "Quantity", "RentPrice" },
                values: new object[] { null, null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3096), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3096), new TimeSpan(0, 0, 0, 0, 0)), 0, 50000.0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AllowRentBeforeDays", "Construction", "CreatedTime", "LastUpdatedTime", "Quantity", "RentPrice" },
                values: new object[] { null, null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3107), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3107), new TimeSpan(0, 0, 0, 0, 0)), 0, 50000.0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AllowRentBeforeDays", "Construction", "CreatedTime", "LastUpdatedTime", "Quantity", "RentPrice" },
                values: new object[] { null, null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3110), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3110), new TimeSpan(0, 0, 0, 0, 0)), 0, 50000.0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AllowRentBeforeDays", "Construction", "CreatedTime", "LastUpdatedTime", "Quantity", "RentPrice" },
                values: new object[] { null, null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3113), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3113), new TimeSpan(0, 0, 0, 0, 0)), 0, 50000.0 });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2895), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2895), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2902), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2905), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2905), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2906), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "7ff2eb7e-35fc-477f-b903-699039d123eb", new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 25, 917, DateTimeKind.Unspecified).AddTicks(8228), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 25, 917, DateTimeKind.Unspecified).AddTicks(8228), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$rW1pjAB8l1pVidMuPRn8c.WgODY7EetM6Urq9WWYDlLdexmsZ5gEm", new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 34, DateTimeKind.Unspecified).AddTicks(9328), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "ba691b50-01f1-4114-92ce-88bfe7e8113a", new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 34, DateTimeKind.Unspecified).AddTicks(9384), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 34, DateTimeKind.Unspecified).AddTicks(9384), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$9aAwvdw/FAz7ICScX94dVerbDKeLeGENuwDQe8KCAJ99aHaKJU59G", new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 157, DateTimeKind.Unspecified).AddTicks(712), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "0aa3be9e-1d21-45bb-b696-0afc1c82dfa6", new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 157, DateTimeKind.Unspecified).AddTicks(743), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 157, DateTimeKind.Unspecified).AddTicks(743), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$LIbs12jfQRZjx/xk/UI9iOEKUR1wGTroViNxIJWCdqM2a8VcNcHke", new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2053), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3178), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3178), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "EndDate", "LastUpdatedTime", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3188), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 5, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3197), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3188), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3196), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "EndDate", "LastUpdatedTime", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3222), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 5, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3224), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3222), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3223), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3226), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3226), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3228), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3228), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3231), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3231), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}

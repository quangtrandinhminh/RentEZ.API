using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Edit_Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(4785), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(4785), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(4834), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(4834), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(4837), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(4837), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(4838), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(4838), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(5546), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(5546), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(5657), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(5657), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(5740), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(5741), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(5905), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(5905), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(4664), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(4664), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(4671), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(4673), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(4673), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(4675), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "187641ff-e1c6-4579-b224-9f71fd5880a8", new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 21, 972, DateTimeKind.Unspecified).AddTicks(7888), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 21, 972, DateTimeKind.Unspecified).AddTicks(7888), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$rGIaMxVFRYv8If9rhO2KjuyFpZY4T8nAaNN2la4KKaBupLZmniL1q", new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 96, DateTimeKind.Unspecified).AddTicks(5760), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "6b785ec2-764f-4522-a1e2-ec04c9cbc6e7", new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 96, DateTimeKind.Unspecified).AddTicks(5781), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 96, DateTimeKind.Unspecified).AddTicks(5781), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$CWqrqDjNzV6NslssaQe6T.PmYviO3a44kZYBHSWQHc2oGvRSuEEhO", new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 225, DateTimeKind.Unspecified).AddTicks(3882), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "82886f33-bc9b-42c9-bea5-a6ad25ad5244", new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 225, DateTimeKind.Unspecified).AddTicks(3903), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 225, DateTimeKind.Unspecified).AddTicks(3903), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$ukoNKyy4quvdXQoZuzISROSyqx10wSy1fOGCrx4RNCg1BNgf5b0tK", new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(3642), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(5984), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(5984), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "EndDate", "LastUpdatedTime", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(5997), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 7, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(6005), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(5997), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(6004), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "EndDate", "LastUpdatedTime", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(6055), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 7, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(6057), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(6055), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(6056), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(6060), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(6060), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(6068), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(6068), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(6071), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 19, 5, 22, 356, DateTimeKind.Unspecified).AddTicks(6071), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Orders");

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
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2793), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2794), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2868), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2869), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2991), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(2991), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3120), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 5, 49, 47, 688, DateTimeKind.Unspecified).AddTicks(3121), new TimeSpan(0, 0, 0, 0, 0)) });

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
    }
}

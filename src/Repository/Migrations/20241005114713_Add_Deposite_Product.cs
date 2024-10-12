using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Add_Deposite_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepositRate",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9593), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9593), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9731), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9731), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9745), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9745), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9747), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9747), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "DepositRate", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9770), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9770), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "DepositRate", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9784), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9784), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "DepositRate", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9796), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9796), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "DepositRate", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9799), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9799), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9560), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9560), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9563), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "e60f9fc1-1b84-439d-b38f-f0119821e08f", new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 350, DateTimeKind.Unspecified).AddTicks(7666), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 350, DateTimeKind.Unspecified).AddTicks(7666), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$oeHzrxQepLFg8LkWoZyA..IiYRbTVhX5fzvifNPI1pdXPf/XuPLfW", new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 477, DateTimeKind.Unspecified).AddTicks(4554), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "94bd9885-af70-428f-bbb1-95c516b34968", new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 477, DateTimeKind.Unspecified).AddTicks(4569), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 477, DateTimeKind.Unspecified).AddTicks(4569), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$mfjVnTSc9RjhmzxzvjU7J.Ivvn9YC8AtCFxZGviWLLV4WOzPdVkru", new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 612, DateTimeKind.Unspecified).AddTicks(8815), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "c44d6468-b16e-44f3-9703-bfe30afb81e5", new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 612, DateTimeKind.Unspecified).AddTicks(8829), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 612, DateTimeKind.Unspecified).AddTicks(8829), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$AfOSwIYQOYPBqj2Sps01XOvUAvdKIvgeVc2ps8xcIuRG.IFWR5c3O", new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9237), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepositRate",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5085), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5085), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5272), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5272), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5275), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5275), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5293), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5293), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5340), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5340), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5353), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5353), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5363), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5363), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5370), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5370), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5018), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5018), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(5023), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "a269205b-0dda-4c9b-8f49-392aa3f24c24", new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 53, 948, DateTimeKind.Unspecified).AddTicks(124), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 53, 948, DateTimeKind.Unspecified).AddTicks(124), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$n1moVGWEO9Q2tzZOL9.u7.8zTCjcp4ZEi7HE0fjzAAeZ991zoKEA2", new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 88, DateTimeKind.Unspecified).AddTicks(8923), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "0117b8b8-6081-45b7-80b1-447b81a07158", new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 88, DateTimeKind.Unspecified).AddTicks(9134), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 88, DateTimeKind.Unspecified).AddTicks(9134), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$.Tk4G2pAWPlAfF0WiPf8x.uENkVB2Bq0EbLz77f3jF4U7lqwnNHpm", new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 246, DateTimeKind.Unspecified).AddTicks(8649), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "70adea64-af28-4ea5-ba69-928ee34cfe60", new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 246, DateTimeKind.Unspecified).AddTicks(8678), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 246, DateTimeKind.Unspecified).AddTicks(8678), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$9voQBExtwliG7bLh8HqZS./ca.P3BuScOdhpUIAKfngiprDwlC3ge", new DateTimeOffset(new DateTime(2024, 9, 29, 5, 52, 54, 415, DateTimeKind.Unspecified).AddTicks(4317), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}

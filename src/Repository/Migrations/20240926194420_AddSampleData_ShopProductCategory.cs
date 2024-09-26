using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddSampleData_ShopProductCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "LastUpdatedBy", "LastUpdatedTime" },
                values: new object[,]
                {
                    { 1, "Handbag", null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5070), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Túi xách", null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5070), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2, "SetOfClothes", null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5297), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Bộ quần áo", null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5297), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 3, "Dress", null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5299), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Váy", null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5299), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 4, "Shoes", null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5309), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Giày dép", null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5309), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime", "OwnerId", "ShopAddress", "ShopAvatar", "ShopEmail", "ShopName", "ShopPhone", "Verified" },
                values: new object[] { 1, null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5006), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5006), new TimeSpan(0, 0, 0, 0, 0)), 2, "123 Street", "https://via.placeholder.com/150", "shopowner@example.com", "Shop Owner", "0123456789", new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5012), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Avatar", "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "https://via.placeholder.com/150", "69fbb4c1-66c7-4041-92cc-dadde47be887", new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 19, 900, DateTimeKind.Unspecified).AddTicks(2647), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 19, 900, DateTimeKind.Unspecified).AddTicks(2647), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$q7UC1g8VVH83bXBYhiBGx.jn6qF./uMywSV9KZOCsrOdesJtvk0AC", new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 18, DateTimeKind.Unspecified).AddTicks(3207), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Avatar", "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "https://via.placeholder.com/150", "0cb4517c-512a-4f61-b820-65a9242cae3d", new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 18, DateTimeKind.Unspecified).AddTicks(3230), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 18, DateTimeKind.Unspecified).AddTicks(3230), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$C6aEMOBCJpLtRj/BSlhPSOVBdazI4ZExt.QkEzHfAID1ynyAuFz5m", new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 137, DateTimeKind.Unspecified).AddTicks(6308), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Avatar", "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "https://via.placeholder.com/150", "498ede0e-7dcc-4c3a-9035-5c8f8a0172cd", new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 137, DateTimeKind.Unspecified).AddTicks(6343), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 137, DateTimeKind.Unspecified).AddTicks(6343), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$MQ5xkUY8DNKBecV9w4QJMeOamap/E4RYEeXkdqLHbQjeGEuzHkUQC", new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(4299), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "Height", "Image", "LastUpdatedBy", "LastUpdatedTime", "Long", "Mass", "Price", "ProductName", "RatingCount", "RentPrice", "RentedCount", "ShopId", "Size", "Width" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5349), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Túi xách đẹp", 15.0, "https://via.placeholder.com/150", null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5349), new TimeSpan(0, 0, 0, 0, 0)), 10.0, 1.0, 100000.0, "Túi xách 1", 0, 50000.0, 0, 1, 10.0, 5.0 },
                    { 2, 1, null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5358), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Túi xách đẹp", 15.0, "https://via.placeholder.com/150", null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5358), new TimeSpan(0, 0, 0, 0, 0)), 10.0, 1.0, 100000.0, "Túi xách 2", 0, 50000.0, 0, 1, 10.0, 5.0 },
                    { 3, 2, null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5367), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Bộ quần áo đẹp", 15.0, "https://via.placeholder.com/150", null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5367), new TimeSpan(0, 0, 0, 0, 0)), 10.0, 1.0, 100000.0, "Bộ quần áo 1", 0, 50000.0, 0, 1, 10.0, 5.0 },
                    { 4, 2, null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5370), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Bộ quần áo đẹp", null, "https://via.placeholder.com/150", null, new DateTimeOffset(new DateTime(2024, 9, 26, 19, 44, 20, 262, DateTimeKind.Unspecified).AddTicks(5370), new TimeSpan(0, 0, 0, 0, 0)), 10.0, 1.0, 100000.0, "Bộ quần áo 2", 0, 50000.0, 0, 1, 10.0, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

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
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Avatar", "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { null, "73be674d-b2d1-46ea-8254-d11f7cfb9b0d", new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 595, DateTimeKind.Unspecified).AddTicks(3391), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 595, DateTimeKind.Unspecified).AddTicks(3391), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$GTPqDtCfcREjiWk/lGS8qeEPR5RiyEefFL7eJlaTqit7lNj6WuROe", new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 727, DateTimeKind.Unspecified).AddTicks(5350), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Avatar", "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { null, "d017d508-5a84-423d-8ff4-5e3a6b83428e", new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 727, DateTimeKind.Unspecified).AddTicks(5961), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 727, DateTimeKind.Unspecified).AddTicks(5961), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$0XNroLVBeRoyOYfD6QoLqeJZ35IEQacacxqdNMGUntlTnyJVvpN2y", new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 855, DateTimeKind.Unspecified).AddTicks(4066), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Avatar", "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { null, "eb56b458-0f06-4adf-bc82-0cd7caa07a7b", new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 855, DateTimeKind.Unspecified).AddTicks(4807), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 855, DateTimeKind.Unspecified).AddTicks(4807), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$82kSauu9BV34LPI8oN.Zk.gedWbdBio7GljQnMTf6UQ85x./PM.S2", new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 980, DateTimeKind.Unspecified).AddTicks(7995), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}

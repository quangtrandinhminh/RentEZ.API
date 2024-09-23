using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Fix_FK_Shop_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "73be674d-b2d1-46ea-8254-d11f7cfb9b0d", new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 595, DateTimeKind.Unspecified).AddTicks(3391), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 595, DateTimeKind.Unspecified).AddTicks(3391), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$GTPqDtCfcREjiWk/lGS8qeEPR5RiyEefFL7eJlaTqit7lNj6WuROe", new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 727, DateTimeKind.Unspecified).AddTicks(5350), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "d017d508-5a84-423d-8ff4-5e3a6b83428e", new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 727, DateTimeKind.Unspecified).AddTicks(5961), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 727, DateTimeKind.Unspecified).AddTicks(5961), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$0XNroLVBeRoyOYfD6QoLqeJZ35IEQacacxqdNMGUntlTnyJVvpN2y", new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 855, DateTimeKind.Unspecified).AddTicks(4066), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "eb56b458-0f06-4adf-bc82-0cd7caa07a7b", new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 855, DateTimeKind.Unspecified).AddTicks(4807), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 855, DateTimeKind.Unspecified).AddTicks(4807), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$82kSauu9BV34LPI8oN.Zk.gedWbdBio7GljQnMTf6UQ85x./PM.S2", new DateTimeOffset(new DateTime(2024, 9, 23, 19, 3, 37, 980, DateTimeKind.Unspecified).AddTicks(7995), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShopId",
                table: "Products",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Shops_ShopId",
                table: "Products",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Shops_ShopId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ShopId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "9980867f-00da-4394-8703-36e2fe390cc9", new DateTimeOffset(new DateTime(2024, 9, 12, 7, 42, 19, 354, DateTimeKind.Unspecified).AddTicks(5858), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 12, 7, 42, 19, 354, DateTimeKind.Unspecified).AddTicks(5858), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$VBQXbiG4MdrABUsX0182puNPw4qGyxyuFgVLEmoPGIe/zxesLDKAu", new DateTimeOffset(new DateTime(2024, 9, 12, 7, 42, 19, 481, DateTimeKind.Unspecified).AddTicks(3371), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "20ec4a33-f984-40e8-8d18-669e7f510e2d", new DateTimeOffset(new DateTime(2024, 9, 12, 7, 42, 19, 484, DateTimeKind.Unspecified).AddTicks(2753), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 12, 7, 42, 19, 484, DateTimeKind.Unspecified).AddTicks(2753), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$/Gko6X.mTb8r92zqxr7lteq9Bv8JLIroaNqjxYZyV0ahZjzND.IsG", new DateTimeOffset(new DateTime(2024, 9, 12, 7, 42, 19, 613, DateTimeKind.Unspecified).AddTicks(2965), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "dede443d-7d17-4158-a3e8-7c9ccd4b3888", new DateTimeOffset(new DateTime(2024, 9, 12, 7, 42, 19, 613, DateTimeKind.Unspecified).AddTicks(3536), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 12, 7, 42, 19, 613, DateTimeKind.Unspecified).AddTicks(3536), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$u10qpO0YGPqfJG/fx2kZgOgpFnTSdWlZIvFodT/8CkqbDh5euOFpW", new DateTimeOffset(new DateTime(2024, 9, 12, 7, 42, 19, 745, DateTimeKind.Unspecified).AddTicks(395), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}

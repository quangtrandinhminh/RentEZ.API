using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Fix_systemconfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_OrderShops_OrderShopId",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Products_ProductId",
                table: "OrderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.RenameTable(
                name: "OrderDetail",
                newName: "OrderDetails");

            migrationBuilder.RenameIndex(
                name: "Index_Id6",
                table: "Vouchers",
                newName: "Index_Id7");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_OrderShopId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderShopId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SystemConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfigs", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "Index_Id6",
                table: "SystemConfigs",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderShops_OrderShopId",
                table: "OrderDetails",
                column: "OrderShopId",
                principalTable: "OrderShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderShops_OrderShopId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropTable(
                name: "SystemConfigs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "OrderDetail");

            migrationBuilder.RenameIndex(
                name: "Index_Id7",
                table: "Vouchers",
                newName: "Index_Id6");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderShopId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_OrderShopId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(3299), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(3299), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(3335), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(3335), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(3337), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(3337), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(3339), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(3339), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4021), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4022), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4125), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4125), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4205), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4205), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4363), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4364), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(3219), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(3219), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(3224), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(3226), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(3226), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(3228), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "50c2039d-d9d1-485d-a8b7-d79ce42838a0", new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 630, DateTimeKind.Unspecified).AddTicks(4371), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 630, DateTimeKind.Unspecified).AddTicks(4371), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$L3LgCa0eTv0.X.ixD61d0.ysbsrlis8q6UVlVwQoOygmZAbiB5FY2", new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 746, DateTimeKind.Unspecified).AddTicks(6648), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "0c5c6aa8-eba5-40fa-8157-ee5f8f357628", new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 746, DateTimeKind.Unspecified).AddTicks(6672), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 746, DateTimeKind.Unspecified).AddTicks(6672), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$nu91kndGzzPEZGpxSPc2qOKx5TR0YLO1seuGBLSnNq4M3iUUImyfK", new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 866, DateTimeKind.Unspecified).AddTicks(3239), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "98fa8d37-5660-452b-aad1-fa32130d1f8e", new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 866, DateTimeKind.Unspecified).AddTicks(3270), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 866, DateTimeKind.Unspecified).AddTicks(3270), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$rV2O2hXJ6ZKgJDuSHF33CetqPqaX7r3EAUsgbu29L7JN7lphA.6ca", new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(2444), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4457), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4457), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "EndDate", "LastUpdatedTime", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4466), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 6, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4473), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4466), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4472), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "EndDate", "LastUpdatedTime", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4519), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 6, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4525), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4519), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4524), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4530), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4530), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4533), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4533), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4536), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 18, 3, 5, 983, DateTimeKind.Unspecified).AddTicks(4536), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_OrderShops_OrderShopId",
                table: "OrderDetail",
                column: "OrderShopId",
                principalTable: "OrderShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Products_ProductId",
                table: "OrderDetail",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Add_Order_Voucher_User_Shop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Shops_ShopId",
                table: "Products");

            migrationBuilder.RenameIndex(
                name: "Index_Id3",
                table: "Shops",
                newName: "Index_Id5");

            migrationBuilder.RenameIndex(
                name: "Index_Id2",
                table: "Products",
                newName: "Index_Id4");

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "Users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNo",
                table: "Users",
                type: "character varying(19)",
                maxLength: 19,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "Users",
                type: "integer",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "Shops",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNo",
                table: "Shops",
                type: "character varying(19)",
                maxLength: 19,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "Shops",
                type: "integer",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ShopId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RentedCount",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RatingCount",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DepositRate",
                table: "Products",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    OrderAddress = table.Column<string>(type: "text", nullable: true),
                    TotalRentPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalDeposit = table.Column<decimal>(type: "numeric", nullable: false),
                    ShipFee = table.Column<decimal>(type: "numeric", nullable: true),
                    ShipSupportFee = table.Column<decimal>(type: "numeric", nullable: true),
                    Tax = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxRate = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    PaymentTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShopId = table.Column<int>(type: "integer", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    MinRentValue = table.Column<decimal>(type: "numeric", nullable: true),
                    MaxDiscountValue = table.Column<decimal>(type: "numeric", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ValueType = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Limit = table.Column<int>(type: "integer", nullable: true),
                    Used = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vouchers_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderShops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShopId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    VoucherId = table.Column<int>(type: "integer", nullable: true),
                    TaxRate = table.Column<decimal>(type: "numeric", nullable: false),
                    Tax = table.Column<decimal>(type: "numeric", nullable: false),
                    CommissionRate = table.Column<decimal>(type: "numeric", nullable: false),
                    Commission = table.Column<decimal>(type: "numeric", nullable: false),
                    ShipFee = table.Column<decimal>(type: "numeric", nullable: false),
                    ShipFeeSupport = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalRentPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    PlatformRentPriceDiscount = table.Column<decimal>(type: "numeric", nullable: false),
                    ShopRentPriceDiscount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalDeposit = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TotalCustomerRefund = table.Column<decimal>(type: "numeric", nullable: true),
                    CustomerReceiveTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    TotalShopReceive = table.Column<decimal>(type: "numeric", nullable: true),
                    ShopReceiveTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CancelBy = table.Column<int>(type: "integer", nullable: true),
                    CancelReason = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderShops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderShops_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderShops_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderShops_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderVoucher",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "integer", nullable: false),
                    PlatformVouchersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderVoucher", x => new { x.OrdersId, x.PlatformVouchersId });
                    table.ForeignKey(
                        name: "FK_OrderVoucher_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderVoucher_Vouchers_PlatformVouchersId",
                        column: x => x.PlatformVouchersId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEntityVoucher",
                columns: table => new
                {
                    UsersId = table.Column<int>(type: "integer", nullable: false),
                    VouchersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntityVoucher", x => new { x.UsersId, x.VouchersId });
                    table.ForeignKey(
                        name: "FK_UserEntityVoucher_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEntityVoucher_Vouchers_VouchersId",
                        column: x => x.VouchersId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    RentQuantity = table.Column<int>(type: "integer", nullable: false),
                    RentDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ReturnDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    RentPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Deposit = table.Column<decimal>(type: "numeric", nullable: false),
                    DepositRate = table.Column<decimal>(type: "numeric", nullable: false),
                    SubTotalRentPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SubTotalDeposit = table.Column<decimal>(type: "numeric", nullable: false),
                    Rating = table.Column<byte>(type: "smallint", nullable: true),
                    ReviewContent = table.Column<string>(type: "text", nullable: true),
                    OrderShopId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_OrderShops_OrderShopId",
                        column: x => x.OrderShopId,
                        principalTable: "OrderShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderTracking",
                columns: table => new
                {
                    OrderTrackingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderShopId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTracking", x => x.OrderTrackingId);
                    table.ForeignKey(
                        name: "FK_OrderTracking_OrderShops_OrderShopId",
                        column: x => x.OrderShopId,
                        principalTable: "OrderShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "CreatedTime", "DepositRate", "LastUpdatedTime", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3096), new TimeSpan(0, 0, 0, 0, 0)), 0.5, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3096), new TimeSpan(0, 0, 0, 0, 0)), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "DepositRate", "LastUpdatedTime", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3107), new TimeSpan(0, 0, 0, 0, 0)), 0.5, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3107), new TimeSpan(0, 0, 0, 0, 0)), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "DepositRate", "LastUpdatedTime", "Quantity", "ShopId" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3110), new TimeSpan(0, 0, 0, 0, 0)), 0.69999999999999996, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3110), new TimeSpan(0, 0, 0, 0, 0)), 0, 2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "DepositRate", "Height", "LastUpdatedTime", "Quantity", "Width" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3113), new TimeSpan(0, 0, 0, 0, 0)), 0.69999999999999996, 15.0, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3113), new TimeSpan(0, 0, 0, 0, 0)), 0, 5.0 });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountName", "AccountNo", "BankId", "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { null, null, null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2895), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2895), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2902), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "AccountName", "AccountNo", "BankId", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime", "OwnerId", "ShopAddress", "ShopAvatar", "ShopEmail", "ShopName", "ShopPhone", "Verified" },
                values: new object[] { 2, null, null, null, null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2905), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2905), new TimeSpan(0, 0, 0, 0, 0)), 3, "123 Street", "https://via.placeholder.com/150", "customershop1@example.com", "Customer Shop", "0123456789", new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2906), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[] { 2, 3, "UserRoleEntity" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountName", "AccountNo", "BankId", "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { null, null, null, "7ff2eb7e-35fc-477f-b903-699039d123eb", new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 25, 917, DateTimeKind.Unspecified).AddTicks(8228), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 25, 917, DateTimeKind.Unspecified).AddTicks(8228), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$rW1pjAB8l1pVidMuPRn8c.WgODY7EetM6Urq9WWYDlLdexmsZ5gEm", new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 34, DateTimeKind.Unspecified).AddTicks(9328), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountName", "AccountNo", "BankId", "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { null, null, null, "ba691b50-01f1-4114-92ce-88bfe7e8113a", new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 34, DateTimeKind.Unspecified).AddTicks(9384), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 34, DateTimeKind.Unspecified).AddTicks(9384), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$9aAwvdw/FAz7ICScX94dVerbDKeLeGENuwDQe8KCAJ99aHaKJU59G", new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 157, DateTimeKind.Unspecified).AddTicks(712), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AccountName", "AccountNo", "BankId", "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { null, null, null, "0aa3be9e-1d21-45bb-b696-0afc1c82dfa6", new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 157, DateTimeKind.Unspecified).AddTicks(743), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 157, DateTimeKind.Unspecified).AddTicks(743), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$LIbs12jfQRZjx/xk/UI9iOEKUR1wGTroViNxIJWCdqM2a8VcNcHke", new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(2053), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "EndDate", "LastUpdatedBy", "LastUpdatedTime", "Limit", "MaxDiscountValue", "MinRentValue", "Name", "ShopId", "StartDate", "Type", "Used", "Value", "ValueType" },
                values: new object[,]
                {
                    { 1, "Voucher1", null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3178), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Giảm 100.000đ phí thuê, RentEZ chào bạn mới", null, null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3178), new TimeSpan(0, 0, 0, 0, 0)), 10, null, null, "Voucher 100k", null, null, 1, 0, 100000m, 2 },
                    { 2, "Voucher2", null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3188), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Giảm 10% phí thuê cho đơn hàng từ Shop 1 có tiền thuê từ 250.000đ tối đa 100.000", new DateTimeOffset(new DateTime(2024, 11, 5, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3197), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3188), new TimeSpan(0, 0, 0, 0, 0)), 10, 100000m, 250000m, "Voucher 10% phí thuê", 1, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3196), new TimeSpan(0, 0, 0, 0, 0)), 1, 0, 0.10m, 1 },
                    { 4, "Voucher4", null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3226), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Giảm 30% phí ship cho đơn hàng có phí thuê từ 100.000đ, tối đa 10.000đ", null, null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3226), new TimeSpan(0, 0, 0, 0, 0)), null, 10000m, 100000m, "Voucher 30% phí ship", null, null, 2, 0, 0.30m, 1 },
                    { 5, "Voucher5", null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3228), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Giảm 50% phí ship cho đơn hàng có phí thuê từ 300.000đ, tối đa 25.000đ", null, null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3228), new TimeSpan(0, 0, 0, 0, 0)), null, 25000m, 300000m, "Voucher 15% phí ship", null, null, 2, 0, 0.5m, 1 },
                    { 6, "Voucher5", null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3231), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Giảm 70% phí ship cho đơn hàng có phí thuê từ 500.000đ, tối đa 75.000đ", null, null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3231), new TimeSpan(0, 0, 0, 0, 0)), null, 75000m, 150000m, "Voucher 15% phí ship", null, null, 2, 0, 0.7m, 1 },
                    { 3, "Voucher3", null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3222), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Giảm 10% phí thuê cho mọi đơn hàng từ Shop 2", new DateTimeOffset(new DateTime(2024, 11, 5, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3224), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3222), new TimeSpan(0, 0, 0, 0, 0)), 10, null, null, "Voucher 10% phí thuê", 2, new DateTimeOffset(new DateTime(2024, 10, 26, 21, 17, 26, 277, DateTimeKind.Unspecified).AddTicks(3223), new TimeSpan(0, 0, 0, 0, 0)), 1, 0, 0.10m, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderShopId",
                table: "OrderDetail",
                column: "OrderShopId");

            migrationBuilder.CreateIndex(
                name: "Index_Id2",
                table: "Orders",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "Index_Id3",
                table: "OrderShops",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderShops_OrderId",
                table: "OrderShops",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderShops_ShopId",
                table: "OrderShops",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderShops_VoucherId",
                table: "OrderShops",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTracking_OrderShopId",
                table: "OrderTracking",
                column: "OrderShopId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderVoucher_PlatformVouchersId",
                table: "OrderVoucher",
                column: "PlatformVouchersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEntityVoucher_VouchersId",
                table: "UserEntityVoucher",
                column: "VouchersId");

            migrationBuilder.CreateIndex(
                name: "Index_Id6",
                table: "Vouchers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_ShopId",
                table: "Vouchers",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Shops_ShopId",
                table: "Products",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Shops_ShopId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "OrderTracking");

            migrationBuilder.DropTable(
                name: "OrderVoucher");

            migrationBuilder.DropTable(
                name: "UserEntityVoucher");

            migrationBuilder.DropTable(
                name: "OrderShops");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Shops");

            migrationBuilder.RenameIndex(
                name: "Index_Id5",
                table: "Shops",
                newName: "Index_Id3");

            migrationBuilder.RenameIndex(
                name: "Index_Id4",
                table: "Products",
                newName: "Index_Id2");

            migrationBuilder.AlterColumn<int>(
                name: "ShopId",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RentedCount",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RatingCount",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "DepositRate",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

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
                columns: new[] { "CreatedTime", "DepositRate", "LastUpdatedTime", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9770), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9770), new TimeSpan(0, 0, 0, 0, 0)), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "DepositRate", "LastUpdatedTime", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9784), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9784), new TimeSpan(0, 0, 0, 0, 0)), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "DepositRate", "LastUpdatedTime", "Quantity", "ShopId" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9796), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9796), new TimeSpan(0, 0, 0, 0, 0)), null, 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "DepositRate", "Height", "LastUpdatedTime", "Quantity", "Width" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9799), new TimeSpan(0, 0, 0, 0, 0)), null, null, new DateTimeOffset(new DateTime(2024, 10, 5, 11, 47, 12, 741, DateTimeKind.Unspecified).AddTicks(9799), new TimeSpan(0, 0, 0, 0, 0)), null, null });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Shops_ShopId",
                table: "Products",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id");
        }
    }
}

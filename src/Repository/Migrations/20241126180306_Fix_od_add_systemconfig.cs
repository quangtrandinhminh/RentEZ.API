using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Fix_od_add_systemconfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Products");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "FeedbackCreatedTime",
                table: "OrderDetail",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "FeedbackLastUpdatedTime",
                table: "OrderDetail",
                type: "timestamp with time zone",
                nullable: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedbackCreatedTime",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "FeedbackLastUpdatedTime",
                table: "OrderDetail");

            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(7411), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(7411), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(7455), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(7455), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(7458), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(7458), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(7460), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(7460), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "RatingCount" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(8474), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(8478), new TimeSpan(0, 0, 0, 0, 0)), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "RatingCount" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(9024), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(9025), new TimeSpan(0, 0, 0, 0, 0)), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "RatingCount" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(9164), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(9165), new TimeSpan(0, 0, 0, 0, 0)), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "RatingCount" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(9374), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(9375), new TimeSpan(0, 0, 0, 0, 0)), 0 });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(7329), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(7329), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(7334), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime", "Verified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(7336), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(7336), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(7338), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "5459caf2-ac97-4b17-883c-62bee3576786", new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 14, 671, DateTimeKind.Unspecified).AddTicks(1201), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 14, 671, DateTimeKind.Unspecified).AddTicks(1201), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$HINMvynEguthV2R1Q6vRTuU6dP5FKIlbjbWDVMeHqgR5WSEpNDlNq", new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 14, 818, DateTimeKind.Unspecified).AddTicks(5883), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "e27ff469-725d-4174-9d24-1b82d38ddae9", new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 14, 818, DateTimeKind.Unspecified).AddTicks(5905), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 14, 818, DateTimeKind.Unspecified).AddTicks(5905), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$hV4Jmz97dDOP7Pncr1v4NuXqU3mOG/aWUneNlZknnjS6NpsJC/3M2", new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 14, 965, DateTimeKind.Unspecified).AddTicks(3017), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "1f67c734-f375-4dcb-a7d6-9f852ab5819e", new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 14, 965, DateTimeKind.Unspecified).AddTicks(3049), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 14, 965, DateTimeKind.Unspecified).AddTicks(3049), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$1L4b04N1qov/JDB55b1hF.kt.8vjSVxtgl.g5v0ARtcUDCoG1i.3a", new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 101, DateTimeKind.Unspecified).AddTicks(6295), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8390), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8390), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "EndDate", "LastUpdatedTime", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8422), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 29, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8438), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8422), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8433), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "EndDate", "LastUpdatedTime", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8510), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 29, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8514), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8510), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8513), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8523), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8523), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8527), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8527), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8531), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 19, 19, 0, 15, 104, DateTimeKind.Unspecified).AddTicks(8531), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}

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
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 1, 20, 729, DateTimeKind.Unspecified).AddTicks(5805), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 1, 20, 729, DateTimeKind.Unspecified).AddTicks(5805), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 1, 20, 729, DateTimeKind.Unspecified).AddTicks(5846), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 1, 20, 729, DateTimeKind.Unspecified).AddTicks(5846), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 1, 20, 729, DateTimeKind.Unspecified).AddTicks(5848), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 1, 20, 729, DateTimeKind.Unspecified).AddTicks(5848), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 20, 1, 20, 729, DateTimeKind.Unspecified).AddTicks(5850), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 1, 20, 729, DateTimeKind.Unspecified).AddTicks(5850), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "8ab99131-2843-4a73-a94f-875f3d5fa78b", new DateTimeOffset(new DateTime(2024, 11, 26, 20, 1, 20, 590, DateTimeKind.Unspecified).AddTicks(6839), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 20, 1, 20, 590, DateTimeKind.Unspecified).AddTicks(6839), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$RBUT3MLw4LFcKjtWG4Lu8u.9XVSaV4/9KecTbHtocOlD188S2oO0W", new DateTimeOffset(new DateTime(2024, 11, 26, 20, 1, 20, 729, DateTimeKind.Unspecified).AddTicks(5121), new TimeSpan(0, 0, 0, 0, 0)) });
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
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 20, 9, 40, 6, 776, DateTimeKind.Unspecified).AddTicks(5657), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 20, 9, 40, 6, 776, DateTimeKind.Unspecified).AddTicks(5657), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 20, 9, 40, 6, 776, DateTimeKind.Unspecified).AddTicks(5882), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 20, 9, 40, 6, 776, DateTimeKind.Unspecified).AddTicks(5882), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 20, 9, 40, 6, 776, DateTimeKind.Unspecified).AddTicks(5884), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 20, 9, 40, 6, 776, DateTimeKind.Unspecified).AddTicks(5884), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 20, 9, 40, 6, 776, DateTimeKind.Unspecified).AddTicks(5886), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 20, 9, 40, 6, 776, DateTimeKind.Unspecified).AddTicks(5886), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "2000fb9c-e042-445c-b7ad-5c45553e8660", new DateTimeOffset(new DateTime(2024, 11, 20, 9, 40, 6, 604, DateTimeKind.Unspecified).AddTicks(2256), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 20, 9, 40, 6, 604, DateTimeKind.Unspecified).AddTicks(2256), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$F4KTGaUFeL4RQDiGmsm46uTy0s6LNmyorXv9Ku3crg/ndAMGGacLe", new DateTimeOffset(new DateTime(2024, 11, 20, 9, 40, 6, 776, DateTimeKind.Unspecified).AddTicks(4767), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Fixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "d0ff9cff-cbef-48df-824b-91135b5f4828", new DateTimeOffset(new DateTime(2024, 9, 10, 17, 38, 59, 248, DateTimeKind.Unspecified).AddTicks(3657), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 10, 17, 38, 59, 248, DateTimeKind.Unspecified).AddTicks(3657), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$DxSfVI4sBoC36uafiDEFQeaRDJXYQ6zvKkBkfQJW2bsBOJN4J5Fje", new DateTimeOffset(new DateTime(2024, 9, 10, 17, 38, 59, 372, DateTimeKind.Unspecified).AddTicks(7870), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "4bd140db-298c-4ec7-95c2-e7e77e1beb1f", new DateTimeOffset(new DateTime(2024, 9, 10, 17, 38, 59, 372, DateTimeKind.Unspecified).AddTicks(8193), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 10, 17, 38, 59, 372, DateTimeKind.Unspecified).AddTicks(8193), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$.dKkdbmA8Mg1WNk55Wee5uySA5MrG.H/rzwxAo57zsClrKjIyhMNG", new DateTimeOffset(new DateTime(2024, 9, 10, 17, 38, 59, 501, DateTimeKind.Unspecified).AddTicks(3040), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "d54ba48e-ee49-4d01-a5f6-31c39507fe3e", new DateTimeOffset(new DateTime(2024, 9, 10, 17, 38, 59, 501, DateTimeKind.Unspecified).AddTicks(3558), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 10, 17, 38, 59, 501, DateTimeKind.Unspecified).AddTicks(3558), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$ROhq4jqVr1vlX2tFyFpYoeJq10Y7a3gmcnvsLepjv2z9SbqKRYFCW", new DateTimeOffset(new DateTime(2024, 9, 10, 17, 38, 59, 637, DateTimeKind.Unspecified).AddTicks(7661), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "26230e2e-139a-4879-ba60-dd4178c6fa73", new DateTimeOffset(new DateTime(2024, 9, 10, 17, 36, 26, 841, DateTimeKind.Unspecified).AddTicks(6264), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 10, 17, 36, 26, 841, DateTimeKind.Unspecified).AddTicks(6264), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$/1dVKJz9GbOPq3PXZHjmrOBhW5Go/EB3M.YmB9UDphJD8Lh1oo2Ny", new DateTimeOffset(new DateTime(2024, 9, 10, 17, 36, 26, 966, DateTimeKind.Unspecified).AddTicks(1216), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "16a31d1a-c97a-42b2-bb24-cf46cb6211dc", new DateTimeOffset(new DateTime(2024, 9, 10, 17, 36, 26, 966, DateTimeKind.Unspecified).AddTicks(1565), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 10, 17, 36, 26, 966, DateTimeKind.Unspecified).AddTicks(1565), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$GT.IWtqR208/HQc2yuPmNu.vxCIQwpjnPe2D7bHa2bvqvQLtfDmKq", new DateTimeOffset(new DateTime(2024, 9, 10, 17, 36, 27, 98, DateTimeKind.Unspecified).AddTicks(2666), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "a1c50048-e74e-485f-a4ac-8cec8f7fa5d8", new DateTimeOffset(new DateTime(2024, 9, 10, 17, 36, 27, 98, DateTimeKind.Unspecified).AddTicks(3196), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 10, 17, 36, 27, 98, DateTimeKind.Unspecified).AddTicks(3196), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$0dK6.Qr9WpP6iNsfJ.TldOO4rPX9lYA064Gk10r3OFxRaHIMRC/9K", new DateTimeOffset(new DateTime(2024, 9, 10, 17, 36, 27, 226, DateTimeKind.Unspecified).AddTicks(4791), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}

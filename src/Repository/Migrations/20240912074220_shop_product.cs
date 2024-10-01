using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class shop_product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "Index_Id",
                table: "RefreshTokens",
                newName: "Index_Id1");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShopEmail = table.Column<string>(type: "text", nullable: true),
                    ShopName = table.Column<string>(type: "text", nullable: true),
                    ShopPhone = table.Column<string>(type: "text", nullable: true),
                    ShopAddress = table.Column<string>(type: "text", nullable: true),
                    ShopAvatar = table.Column<string>(type: "text", nullable: true),
                    Verified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    OwnerId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shops_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductName = table.Column<string>(type: "text", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Size = table.Column<double>(type: "double precision", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: true),
                    RentPrice = table.Column<double>(type: "double precision", nullable: true),
                    RentedCount = table.Column<int>(type: "integer", nullable: true),
                    RatingCount = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Mass = table.Column<double>(type: "double precision", nullable: true),
                    Long = table.Column<double>(type: "double precision", nullable: true),
                    Width = table.Column<double>(type: "double precision", nullable: true),
                    Height = table.Column<double>(type: "double precision", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "Index_Id",
                table: "Categories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Index_Id2",
                table: "Products",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "Index_Id3",
                table: "Shops",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shops_OwnerId",
                table: "Shops",
                column: "OwnerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.RenameIndex(
                name: "Index_Id1",
                table: "RefreshTokens",
                newName: "Index_Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "91fc2b2c-630f-4355-86b2-d85800a5d116", new DateTimeOffset(new DateTime(2024, 8, 31, 8, 16, 55, 369, DateTimeKind.Unspecified).AddTicks(5579), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 31, 8, 16, 55, 369, DateTimeKind.Unspecified).AddTicks(5579), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$JHVzlZA8IxyjAOrhj7YqVObMbmziPJrKp75HpDugJIXZuUeQPQBDy", new DateTimeOffset(new DateTime(2024, 8, 31, 8, 16, 55, 485, DateTimeKind.Unspecified).AddTicks(1363), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "70654d8c-6328-47d5-b53a-37ad1b0ba797", new DateTimeOffset(new DateTime(2024, 8, 31, 8, 16, 55, 485, DateTimeKind.Unspecified).AddTicks(1827), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 31, 8, 16, 55, 485, DateTimeKind.Unspecified).AddTicks(1827), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$Kbk8JjimCo8NA2jfOYKbfO2MTRWnpDrsK6d4KNZOVPoBlns2gRoge", new DateTimeOffset(new DateTime(2024, 8, 31, 8, 16, 55, 605, DateTimeKind.Unspecified).AddTicks(5077), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "Verified" },
                values: new object[] { "7935f452-4a32-4aec-8e00-dc06a8c2b716", new DateTimeOffset(new DateTime(2024, 8, 31, 8, 16, 55, 605, DateTimeKind.Unspecified).AddTicks(5513), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 31, 8, 16, 55, 605, DateTimeKind.Unspecified).AddTicks(5513), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$xslOq7Me23tD/O42VVaGyuZzmBWAgyi6U2Ej7Ghjrh/JNgt1BhqN6", new DateTimeOffset(new DateTime(2024, 8, 31, 8, 16, 55, 723, DateTimeKind.Unspecified).AddTicks(7936), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}

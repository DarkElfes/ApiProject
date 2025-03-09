using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiProject.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhoneCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<int>(type: "INTEGER", nullable: false),
                    ImgUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    Stock = table.Column<short>(type: "INTEGER", nullable: false),
                    Sold = table.Column<short>(type: "INTEGER", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneCases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    IdToken = table.Column<string>(type: "TEXT", nullable: true),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: true),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PhoneCases",
                columns: new[] { "Id", "Color", "CreatedAt", "ImgUrl", "Model", "Name", "Price", "Sold", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Green", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://ae-pic-a1.aliexpress-media.com/kf/S0082d2ca5c48496c8df2d0b69a3edaddq.jpg_960x960q75.jpg_.avif", 1, "Shockproof Armor Matte Case", 81.0, (short)0, (short)10, null },
                    { 2, "Black", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://ae-pic-a1.aliexpress-media.com/kf/S10f49ffaa7ea434ca62700fa04444087l.jpg_960x960q75.jpg_.avif", 4, "Luxury Liquid Silicone Phone Case ", 92.0, (short)0, (short)10, null },
                    { 3, "Red Brown", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://ae-pic-a1.aliexpress-media.com/kf/S4fc26c62c8104b288029a0852d1117cc6.jpg_960x960q75.jpg_.avif", 4, "Luxury Liquid Silicone For Magsafe Phone Case", 255.0, (short)0, (short)10, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IdToken", "PasswordHash", "RefreshToken", "Role", "UpdatedAt", "Username" },
                values: new object[] { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@caseshop.com", null, "AQAAAAIAAYagAAAAEOMiV4vZOmimSUP3whM3EdotmdNNlNiPUySw18aeStR3+nVzltjeGeoRqT93FbCtVw==", null, "Admin", null, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneCases");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

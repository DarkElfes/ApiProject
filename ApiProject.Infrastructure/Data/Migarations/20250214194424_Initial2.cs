using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiProject.Infrastructure.Data.Migarations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
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
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: true),
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
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
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
                columns: new[] { "Id", "Color", "Created", "CreatedAt", "ImgUrl", "LastUpdated", "Model", "Name", "Price", "Sold", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Green", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://ae-pic-a1.aliexpress-media.com/kf/S0082d2ca5c48496c8df2d0b69a3edaddq.jpg_960x960q75.jpg_.avif", null, 1, "Shockproof Armor Matte Case", 81.0, (short)0, (short)10, null },
                    { 2, "Black", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://ae-pic-a1.aliexpress-media.com/kf/S10f49ffaa7ea434ca62700fa04444087l.jpg_960x960q75.jpg_.avif", null, 4, "Luxury Liquid Silicone Phone Case ", 92.0, (short)0, (short)10, null },
                    { 3, "Red Brown", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://ae-pic-a1.aliexpress-media.com/kf/S4fc26c62c8104b288029a0852d1117cc6.jpg_960x960q75.jpg_.avif", null, 4, "Luxury Liquid Silicone For Magsafe Phone Case", 255.0, (short)0, (short)10, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "Role", "UpdatedAt", "Username" },
                values: new object[] { 1, new DateTime(2025, 2, 14, 19, 44, 24, 71, DateTimeKind.Utc).AddTicks(7457), "admin@caseshop.com", "AQAAAAIAAYagAAAAEOMiV4vZOmimSUP3whM3EdotmdNNlNiPUySw18aeStR3+nVzltjeGeoRqT93FbCtVw==", "Admin", null, "Admin" });
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

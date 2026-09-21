using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniB2B.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSliderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LinkUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "ManufacturerCode", "Price", "ProductCode", "ProductName", "SpecialCode1" },
                values: new object[] { new DateTime(2026, 9, 19, 16, 49, 26, 789, DateTimeKind.Local).AddTicks(516), "Yüksek performanslı iş bilgisayarı.", "/images/dell_laptop_product_1789753837200.jpg", "DELL-XPS-2026", 45000m, "LP-XPS15", "Dell XPS 15 Laptop", "YENI-NESIL" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "ManufacturerCode", "Price", "ProductCode", "ProductName", "StockQuantity" },
                values: new object[] { new DateTime(2026, 9, 19, 16, 49, 26, 789, DateTimeKind.Local).AddTicks(532), "Ergonomik kablosuz mouse.", "/images/logitech_mouse_product_1789753847396.jpg", "LOGI-MX3", 2500m, "MS-MX3", "Logitech MX Master 3", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "CriticalStockLevel", "Description", "ImageUrl", "ManufacturerCode", "Price", "ProductCode", "ProductName", "SpecialCode1", "StockQuantity" },
                values: new object[] { new DateTime(2026, 9, 19, 16, 49, 26, 789, DateTimeKind.Local).AddTicks(533), 100, "Premium kalite fotokopi kağıdı.", "/images/a4_paper_product_1789753857408.jpg", null, 120m, "KP-A4", "A4 Fotokopi Kağıdı (500'lü)", "TOPTAN", 500 });

            migrationBuilder.InsertData(
                table: "Sliders",
                columns: new[] { "Id", "Description", "ImageUrl", "IsActive", "LinkUrl", "OrderIndex", "Title" },
                values: new object[,]
                {
                    { 1, null, "/images/b2b_banner_welcome_1789753868740.jpg", true, null, 1, "Banner 1" },
                    { 2, null, "/images/b2b_banner_stock_1789753880181.jpg", true, null, 2, "Banner 2" },
                    { 3, null, "/images/b2b_banner_discount_1789753891697.jpg", true, null, 3, "Banner 3" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 19, 16, 49, 26, 789, DateTimeKind.Local).AddTicks(555));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 19, 16, 49, 26, 789, DateTimeKind.Local).AddTicks(557));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "ManufacturerCode", "Price", "ProductCode", "ProductName", "SpecialCode1" },
                values: new object[] { new DateTime(2026, 9, 18, 13, 51, 7, 638, DateTimeKind.Local).AddTicks(4232), null, "https://via.placeholder.com/150", "DL-XPS", 25000m, "PRD-001", "Laptop", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "ManufacturerCode", "Price", "ProductCode", "ProductName", "StockQuantity" },
                values: new object[] { new DateTime(2026, 9, 18, 13, 51, 7, 638, DateTimeKind.Local).AddTicks(4234), null, "https://via.placeholder.com/150", "LG-M", 500m, "PRD-002", "Mouse", 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "CriticalStockLevel", "Description", "ImageUrl", "ManufacturerCode", "Price", "ProductCode", "ProductName", "SpecialCode1", "StockQuantity" },
                values: new object[] { new DateTime(2026, 9, 18, 13, 51, 7, 638, DateTimeKind.Local).AddTicks(4236), 50, null, "https://via.placeholder.com/150", "CP-A4", 100m, "PRD-003", "A4 Kağıt", null, 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 18, 13, 51, 7, 638, DateTimeKind.Local).AddTicks(4263));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 18, 13, 51, 7, 638, DateTimeKind.Local).AddTicks(4264));
        }
    }
}

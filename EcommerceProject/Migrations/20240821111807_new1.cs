using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcommerceProject.Migrations
{
    /// <inheritdoc />
    public partial class new1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "T-Shirts");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Jeans");

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpiryDate",
                value: new DateTime(2024, 9, 21, 14, 18, 6, 498, DateTimeKind.Local).AddTicks(6971));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpiryDate",
                value: new DateTime(2024, 10, 21, 14, 18, 6, 498, DateTimeKind.Local).AddTicks(7003));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpiryDate",
                value: new DateTime(2024, 11, 21, 14, 18, 6, 498, DateTimeKind.Local).AddTicks(7005));

            migrationBuilder.InsertData(
                table: "ModelInformationModels",
                columns: new[] { "Id", "ChestSize", "Height", "HipSize", "JeansSize", "ShirtSize" },
                values: new object[,]
                {
                    { 1, "100", "180", "95", "32", "M" },
                    { 2, "95", "175", "90", "30", "S" }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageUrl", "ProductId" },
                values: new object[,]
                {
                    { 1, "img/shop-details/product-big.png", 1 },
                    { 2, "img/shop-details/product-big-2.png", 1 },
                    { 3, "img/shop-details/product-big-3.png", 2 },
                    { 4, "img/shop-details/product-big-4.png", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "Description", "ImageUrl", "ModelInformationId", "Name", "Price", "Rating", "ReviewCount", "Size", "StockQuantity" },
                values: new object[] { "Red,Blue,Green", "A comfortable and stylish T-Shirt.", "tshirt1.jpg", 1, "Casual T-Shirt", 19.99m, 4.0, 10, "M,L,XL", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Color", "Description", "ImageUrl", "ModelInformationId", "Name", "Price", "Rating", "ReviewCount", "Size", "StockQuantity" },
                values: new object[] { "Blue,Black", "Durable and stylish jeans for everyday wear.", "jeans1.jpg", 2, "Classic Jeans", 49.99m, 5.0, 20, "30,32,34", 30 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ModelInformationModels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ModelInformationModels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Category1");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Category2");

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpiryDate",
                value: new DateTime(2024, 9, 21, 10, 12, 33, 696, DateTimeKind.Local).AddTicks(9678));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpiryDate",
                value: new DateTime(2024, 10, 21, 10, 12, 33, 696, DateTimeKind.Local).AddTicks(9695));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpiryDate",
                value: new DateTime(2024, 11, 21, 10, 12, 33, 696, DateTimeKind.Local).AddTicks(9697));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "Description", "ImageUrl", "ModelInformationId", "Name", "Price", "Rating", "ReviewCount", "Size", "StockQuantity" },
                values: new object[] { "Red", "Description1", "image1.jpg", 0, "Product1", 10.0m, 0.0, 0, "M", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Color", "Description", "ImageUrl", "ModelInformationId", "Name", "Price", "Rating", "ReviewCount", "Size", "StockQuantity" },
                values: new object[] { "Blue", "Description2", "image2.jpg", 0, "Product2", 20.0m, 0.0, 0, "L", 200 });
        }
    }
}

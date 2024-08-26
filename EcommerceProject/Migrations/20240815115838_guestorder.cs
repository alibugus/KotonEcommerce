using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceProject.Migrations
{
    /// <inheritdoc />
    public partial class guestorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuestOrderModelId",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GuestOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuestOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestOrderDetails_GuestOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "GuestOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpiryDate",
                value: new DateTime(2024, 9, 15, 14, 58, 36, 827, DateTimeKind.Local).AddTicks(7097));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpiryDate",
                value: new DateTime(2024, 10, 15, 14, 58, 36, 827, DateTimeKind.Local).AddTicks(7184));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpiryDate",
                value: new DateTime(2024, 11, 15, 14, 58, 36, 827, DateTimeKind.Local).AddTicks(7186));

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_GuestOrderModelId",
                table: "OrderDetails",
                column: "GuestOrderModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestOrderDetails_OrderId",
                table: "GuestOrderDetails",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_GuestOrders_GuestOrderModelId",
                table: "OrderDetails",
                column: "GuestOrderModelId",
                principalTable: "GuestOrders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_GuestOrders_GuestOrderModelId",
                table: "OrderDetails");

            migrationBuilder.DropTable(
                name: "GuestOrderDetails");

            migrationBuilder.DropTable(
                name: "GuestOrders");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_GuestOrderModelId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "GuestOrderModelId",
                table: "OrderDetails");

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpiryDate",
                value: new DateTime(2024, 9, 15, 13, 48, 43, 136, DateTimeKind.Local).AddTicks(7201));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpiryDate",
                value: new DateTime(2024, 10, 15, 13, 48, 43, 136, DateTimeKind.Local).AddTicks(7219));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpiryDate",
                value: new DateTime(2024, 11, 15, 13, 48, 43, 136, DateTimeKind.Local).AddTicks(7220));
        }
    }
}

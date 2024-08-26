using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceProject.Migrations
{
    /// <inheritdoc />
    public partial class last : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_GuestOrders_GuestOrderModelId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_GuestOrderModelId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "GuestOrderModelId",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<string>(
                name: "GuestId",
                table: "GuestOrders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpiryDate",
                value: new DateTime(2024, 9, 19, 8, 12, 23, 87, DateTimeKind.Local).AddTicks(6223));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpiryDate",
                value: new DateTime(2024, 10, 19, 8, 12, 23, 87, DateTimeKind.Local).AddTicks(6243));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpiryDate",
                value: new DateTime(2024, 11, 19, 8, 12, 23, 87, DateTimeKind.Local).AddTicks(6244));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuestOrderModelId",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GuestId",
                table: "GuestOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_GuestOrders_GuestOrderModelId",
                table: "OrderDetails",
                column: "GuestOrderModelId",
                principalTable: "GuestOrders",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceProject.Migrations
{
    /// <inheritdoc />
    public partial class asdafsddsadSAD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "UserCoupons");

            migrationBuilder.AddColumn<DateTime>(
                name: "UsedDate",
                table: "UserCoupons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpiryDate",
                value: new DateTime(2024, 9, 8, 14, 57, 21, 838, DateTimeKind.Local).AddTicks(5048));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpiryDate",
                value: new DateTime(2024, 10, 8, 14, 57, 21, 838, DateTimeKind.Local).AddTicks(5109));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpiryDate",
                value: new DateTime(2024, 11, 8, 14, 57, 21, 838, DateTimeKind.Local).AddTicks(5111));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsedDate",
                table: "UserCoupons");

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "UserCoupons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpiryDate",
                value: new DateTime(2024, 9, 8, 14, 32, 48, 97, DateTimeKind.Local).AddTicks(8622));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpiryDate",
                value: new DateTime(2024, 10, 8, 14, 32, 48, 97, DateTimeKind.Local).AddTicks(8642));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpiryDate",
                value: new DateTime(2024, 11, 8, 14, 32, 48, 97, DateTimeKind.Local).AddTicks(8644));
        }
    }
}

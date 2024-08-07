using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcommerceProject.Migrations
{
    /// <inheritdoc />
    public partial class addmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            


            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "Code", "DiscountAmount", "ExpiryDate", "IsActive" },
                values: new object[,]
                {
                    { 1, "WELCOME10", 10m, new DateTime(2024, 9, 7, 13, 23, 31, 233, DateTimeKind.Local).AddTicks(3529), true },
                    { 2, "SUMMER20", 20m, new DateTime(2024, 10, 7, 13, 23, 31, 233, DateTimeKind.Local).AddTicks(3549), true },
                    { 3, "FALL30", 30m, new DateTime(2024, 11, 7, 13, 23, 31, 233, DateTimeKind.Local).AddTicks(3551), true }
                });

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropTable(
                name: "Coupons");

            
           

        

        }
    }
}

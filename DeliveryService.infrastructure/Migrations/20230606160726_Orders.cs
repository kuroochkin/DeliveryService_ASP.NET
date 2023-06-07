using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.infrastructure.Migrations
{
    public partial class Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderEntityId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderEntityId",
                table: "Products",
                column: "OrderEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderEntityId",
                table: "Products",
                column: "OrderEntityId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderEntityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderEntityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderEntityId",
                table: "Products");
        }
    }
}

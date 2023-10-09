using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.Services.PaymentAPI.Infrastructure.Migrations
{
    public partial class newarch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CardNumber = table.Column<string>(type: "text", nullable: false),
                    CVV = table.Column<string>(type: "text", nullable: true),
                    ExpiryMonthYear = table.Column<string>(type: "text", nullable: false),
                    OrderTotalSum = table.Column<double>(type: "double precision", nullable: false),
                    CartTotalItems = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}

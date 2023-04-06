using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.infrastructure.Migrations
{
    public partial class EnumEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropColumn(
					name: "UserType",
					table: "Users");

			migrationBuilder.DropColumn(
					name: "GetUserTypeToString",
					table: "Users");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

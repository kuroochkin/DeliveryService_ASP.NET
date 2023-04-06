using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.infrastructure.Migrations
{
    public partial class EnumUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropColumn(
					name: "UserType",
					table: "Users");

			migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Users");

			migrationBuilder.AddColumn<string>(
				name: "UserType",
				table: "Users",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");

		}
    }
}

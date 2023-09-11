using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.infrastructure.Migrations
{
    public partial class removeStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_StorageFiles_StorageFileFileId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "StorageFiles");

            migrationBuilder.DropIndex(
                name: "IX_Products_StorageFileFileId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StorageFileFileId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "StorageFileFileId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StorageFiles",
                columns: table => new
                {
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BucketName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageFiles", x => x.FileId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_StorageFileFileId",
                table: "Products",
                column: "StorageFileFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_StorageFiles_StorageFileFileId",
                table: "Products",
                column: "StorageFileFileId",
                principalTable: "StorageFiles",
                principalColumn: "FileId");
        }
    }
}

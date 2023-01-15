using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChocolateData.Migrations
{
    public partial class CategoryMainPhotoAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainProductId",
                table: "Categories",
                newName: "MainPhotoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainPhotoId",
                table: "Categories",
                newName: "MainProductId");
        }
    }
}

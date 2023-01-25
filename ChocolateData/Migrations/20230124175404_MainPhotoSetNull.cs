using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChocolateData.Migrations
{
    /// <inheritdoc />
    public partial class MainPhotoSetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Photos_MainPhotoId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Photos_MainPhotoId",
                table: "Products",
                column: "MainPhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Photos_MainPhotoId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Photos_MainPhotoId",
                table: "Products",
                column: "MainPhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChocolateData.Migrations
{
    public partial class ImageIntoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathToPhoto",
                table: "Photos");

            migrationBuilder.AddColumn<Guid>(
                name: "MainPhotoId",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Photos",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Products_MainPhotoId",
                table: "Products",
                column: "MainPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Photos_MainPhotoId",
                table: "Products",
                column: "MainPhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Photos_MainPhotoId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MainPhotoId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MainPhotoId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "PathToPhoto",
                table: "Photos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

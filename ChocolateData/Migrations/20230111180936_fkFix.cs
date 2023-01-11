using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChocolateData.Migrations
{
    public partial class fkFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Products_ProductEntityId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ProductEntityId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ProductEntityId",
                table: "Photos");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ProductId",
                table: "Photos",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Products_ProductId",
                table: "Photos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Products_ProductId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ProductId",
                table: "Photos");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductEntityId",
                table: "Photos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ProductEntityId",
                table: "Photos",
                column: "ProductEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Products_ProductEntityId",
                table: "Photos",
                column: "ProductEntityId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

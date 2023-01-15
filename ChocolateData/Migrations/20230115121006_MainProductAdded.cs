using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChocolateData.Migrations
{
    public partial class MainProductAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MainProductId",
                table: "Categories",
                type: "uuid",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainProductId",
                table: "Categories");
        }
    }
}

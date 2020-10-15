using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolRepairSystem.Models.Migrations
{
    public partial class addMenuspath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Menus",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Menus");
        }
    }
}

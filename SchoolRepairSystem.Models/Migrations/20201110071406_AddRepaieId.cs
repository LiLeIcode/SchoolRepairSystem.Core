using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolRepairSystem.Models.Migrations
{
    public partial class AddRepaieId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserWareHouseId",
                table: "UserWareHouses",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserWareHouseId",
                table: "UserWareHouses");
        }
    }
}

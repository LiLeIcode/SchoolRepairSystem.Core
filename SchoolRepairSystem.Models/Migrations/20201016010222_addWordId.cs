using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolRepairSystem.Models.Migrations
{
    public partial class addWordId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WorkerId",
                table: "RoleReportForRepairs",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "RoleReportForRepairs");
        }
    }
}

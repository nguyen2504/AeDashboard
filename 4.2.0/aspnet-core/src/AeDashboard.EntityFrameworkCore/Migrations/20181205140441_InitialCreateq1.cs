using Microsoft.EntityFrameworkCore.Migrations;

namespace AeDashboard.Migrations
{
    public partial class InitialCreateq1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Documents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Documents");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AeDashboard.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalendarView",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BeginDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Work = table.Column<string>(nullable: false),
                    Admin = table.Column<string>(nullable: true),
                    IdAdmins = table.Column<string>(nullable: true),
                    Users = table.Column<string>(nullable: false),
                    IdUsers = table.Column<string>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Weekend = table.Column<int>(nullable: false),
                    IsAcive = table.Column<bool>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    Weekdays = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarView", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Important = table.Column<bool>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    Notifications = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    IdUser = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarView");

            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}

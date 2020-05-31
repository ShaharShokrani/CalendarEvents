using Microsoft.EntityFrameworkCore.Migrations;

namespace CalendarEvents.DataAccess
{
    public partial class Base64Id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Base64Id",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64Id",
                table: "Events");
        }
    }
}

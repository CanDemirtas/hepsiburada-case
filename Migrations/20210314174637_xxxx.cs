using Microsoft.EntityFrameworkCore.Migrations;

namespace HepsiburadaCase.Migrations
{
    public partial class xxxx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentDuration",
                table: "Campaigns",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentDuration",
                table: "Campaigns");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace HepsiburadaCase.Migrations
{
    public partial class xxxxdadawdawd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CurrentProductPrice",
                table: "Campaigns",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentProductPrice",
                table: "Campaigns");
        }
    }
}

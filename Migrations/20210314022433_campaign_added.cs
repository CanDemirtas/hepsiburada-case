using Microsoft.EntityFrameworkCore.Migrations;

namespace HepsiburadaCase.Migrations
{
    public partial class campaign_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Orders");

            migrationBuilder.AddColumn<double>(
                name: "ProductSoldPrice",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Campaigns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriceManipulationLimit",
                table: "Campaigns",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Campaigns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_ProductId",
                table: "Campaigns",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Products_ProductId",
                table: "Campaigns",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Products_ProductId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_ProductId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "ProductSoldPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "PriceManipulationLimit",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Campaigns");

            migrationBuilder.AddColumn<double>(
                name: "ItemPrice",
                table: "Orders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "Orders",
                type: "text",
                nullable: true);
        }
    }
}

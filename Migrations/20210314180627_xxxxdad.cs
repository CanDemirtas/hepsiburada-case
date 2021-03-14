using Microsoft.EntityFrameworkCore.Migrations;

namespace HepsiburadaCase.Migrations
{
    public partial class xxxxdad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CampaignId",
                table: "Orders",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Campaigns_CampaignId",
                table: "Orders",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Campaigns_CampaignId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CampaignId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Orders");
        }
    }
}

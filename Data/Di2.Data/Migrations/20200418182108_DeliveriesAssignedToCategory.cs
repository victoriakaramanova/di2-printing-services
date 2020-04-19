using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2.Data.Migrations
{
    public partial class DeliveriesAssignedToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_CategoryId",
                table: "Deliveries",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Categories_CategoryId",
                table: "Deliveries",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Categories_CategoryId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_CategoryId",
                table: "Deliveries");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2.Data.Migrations
{
    public partial class deliveryMaterialChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Deliveries");

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "Deliveries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "Deliveries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_MaterialId",
                table: "Deliveries",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Materials_MaterialId",
                table: "Deliveries",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Materials_MaterialId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_MaterialId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "Deliveries");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

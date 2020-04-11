using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2.Data.Migrations
{
    public partial class seeDeliveriesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Categories_CategoryId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_SubCategories_SubCategoryId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_CategoryId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_SubCategoryId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "ExtraInfo",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Deliveries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraInfo",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_CategoryId",
                table: "Deliveries",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_SubCategoryId",
                table: "Deliveries",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Categories_CategoryId",
                table: "Deliveries",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_SubCategories_SubCategoryId",
                table: "Deliveries",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

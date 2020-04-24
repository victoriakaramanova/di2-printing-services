using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2.Data.Migrations
{
    public partial class nullableValues1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryMaterial_Materials_MaterialId",
                table: "SubCategoryMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryMaterial_SubCategories_SubCategoryId",
                table: "SubCategoryMaterial");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryMaterial_Materials_MaterialId",
                table: "SubCategoryMaterial",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryMaterial_SubCategories_SubCategoryId",
                table: "SubCategoryMaterial",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryMaterial_Materials_MaterialId",
                table: "SubCategoryMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryMaterial_SubCategories_SubCategoryId",
                table: "SubCategoryMaterial");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryMaterial_Materials_MaterialId",
                table: "SubCategoryMaterial",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryMaterial_SubCategories_SubCategoryId",
                table: "SubCategoryMaterial",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

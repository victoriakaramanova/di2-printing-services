using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2.Data.Migrations
{
    public partial class deliveriesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deiveries_Categories_CategoryId",
                table: "Deiveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deiveries_SubCategories_SubCategoryId",
                table: "Deiveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deiveries_AspNetUsers_UserId",
                table: "Deiveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deiveries",
                table: "Deiveries");

            migrationBuilder.RenameTable(
                name: "Deiveries",
                newName: "Deliveries");

            migrationBuilder.RenameIndex(
                name: "IX_Deiveries_UserId",
                table: "Deliveries",
                newName: "IX_Deliveries_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Deiveries_SubCategoryId",
                table: "Deliveries",
                newName: "IX_Deliveries_SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Deiveries_IsDeleted",
                table: "Deliveries",
                newName: "IX_Deliveries_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Deiveries_CategoryId",
                table: "Deliveries",
                newName: "IX_Deliveries_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deliveries",
                table: "Deliveries",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_UserId",
                table: "Deliveries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Categories_CategoryId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_SubCategories_SubCategoryId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_UserId",
                table: "Deliveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deliveries",
                table: "Deliveries");

            migrationBuilder.RenameTable(
                name: "Deliveries",
                newName: "Deiveries");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_UserId",
                table: "Deiveries",
                newName: "IX_Deiveries_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_SubCategoryId",
                table: "Deiveries",
                newName: "IX_Deiveries_SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_IsDeleted",
                table: "Deiveries",
                newName: "IX_Deiveries_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_CategoryId",
                table: "Deiveries",
                newName: "IX_Deiveries_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deiveries",
                table: "Deiveries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deiveries_Categories_CategoryId",
                table: "Deiveries",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deiveries_SubCategories_SubCategoryId",
                table: "Deiveries",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deiveries_AspNetUsers_UserId",
                table: "Deiveries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

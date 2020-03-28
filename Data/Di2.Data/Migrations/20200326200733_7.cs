using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2.Data.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderSuppliers_Materials_MaterialId",
                table: "OrderSuppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderSuppliers_Suppliers_SupplierId",
                table: "OrderSuppliers");

            migrationBuilder.DropIndex(
                name: "IX_OrderSuppliers_MaterialId",
                table: "OrderSuppliers");

            migrationBuilder.DropIndex(
                name: "IX_OrderSuppliers_SupplierId",
                table: "OrderSuppliers");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "OrderSuppliers");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "OrderSuppliers");

            migrationBuilder.AddColumn<int>(
                name: "PriceListId",
                table: "OrderSuppliers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriceListMaterialId",
                table: "OrderSuppliers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriceListSupplierId",
                table: "OrderSuppliers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderSuppliers_PriceListMaterialId_PriceListSupplierId",
                table: "OrderSuppliers",
                columns: new[] { "PriceListMaterialId", "PriceListSupplierId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSuppliers_PriceLists_PriceListMaterialId_PriceListSupplierId",
                table: "OrderSuppliers",
                columns: new[] { "PriceListMaterialId", "PriceListSupplierId" },
                principalTable: "PriceLists",
                principalColumns: new[] { "MaterialId", "SupplierId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderSuppliers_PriceLists_PriceListMaterialId_PriceListSupplierId",
                table: "OrderSuppliers");

            migrationBuilder.DropIndex(
                name: "IX_OrderSuppliers_PriceListMaterialId_PriceListSupplierId",
                table: "OrderSuppliers");

            migrationBuilder.DropColumn(
                name: "PriceListId",
                table: "OrderSuppliers");

            migrationBuilder.DropColumn(
                name: "PriceListMaterialId",
                table: "OrderSuppliers");

            migrationBuilder.DropColumn(
                name: "PriceListSupplierId",
                table: "OrderSuppliers");

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "OrderSuppliers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "OrderSuppliers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderSuppliers_MaterialId",
                table: "OrderSuppliers",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSuppliers_SupplierId",
                table: "OrderSuppliers",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSuppliers_Materials_MaterialId",
                table: "OrderSuppliers",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSuppliers_Suppliers_SupplierId",
                table: "OrderSuppliers",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

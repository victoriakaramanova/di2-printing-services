using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2.Data.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderSuppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    MaterialId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSuppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSuppliers_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderSuppliers_OrderStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderSuppliers_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderSuppliers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatuses_IsDeleted",
                table: "OrderStatuses",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSuppliers_IsDeleted",
                table: "OrderSuppliers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSuppliers_MaterialId",
                table: "OrderSuppliers",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSuppliers_StatusId",
                table: "OrderSuppliers",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSuppliers_SupplierId",
                table: "OrderSuppliers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSuppliers_UserId",
                table: "OrderSuppliers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderSuppliers");

            migrationBuilder.DropTable(
                name: "OrderStatuses");
        }
    }
}

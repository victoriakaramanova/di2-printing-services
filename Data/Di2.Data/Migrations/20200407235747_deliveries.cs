using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2.Data.Migrations
{
    public partial class deliveries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Totals");

            migrationBuilder.CreateTable(
                name: "Deiveries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ExtraInfo = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    SubCategoryId = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deiveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deiveries_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deiveries_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deiveries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplyOrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    OrderId = table.Column<int>(nullable: false),
                    OrderSupplierId = table.Column<int>(nullable: true),
                    OrderStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyOrderStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplyOrderStatuses_OrderSuppliers_OrderSupplierId",
                        column: x => x.OrderSupplierId,
                        principalTable: "OrderSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deiveries_CategoryId",
                table: "Deiveries",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Deiveries_IsDeleted",
                table: "Deiveries",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Deiveries_SubCategoryId",
                table: "Deiveries",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Deiveries_UserId",
                table: "Deiveries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyOrderStatuses_OrderSupplierId",
                table: "SupplyOrderStatuses",
                column: "OrderSupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deiveries");

            migrationBuilder.DropTable(
                name: "SupplyOrderStatuses");

            migrationBuilder.CreateTable(
                name: "Totals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Totals", x => x.Id);
                });
        }
    }
}

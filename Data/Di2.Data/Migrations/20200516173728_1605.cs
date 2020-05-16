using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2.Data.Migrations
{
    public partial class _1605 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAddresss",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "Receipts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "Receipts");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddresss",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

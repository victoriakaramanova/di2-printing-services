using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2.Data.Migrations
{
    public partial class DeliveriesRestoredProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Deliveries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraInfo",
                table: "Deliveries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Deliveries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "Deliveries",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "Deliveries",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

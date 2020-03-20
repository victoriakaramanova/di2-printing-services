using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2.Data.Migrations
{
    public partial class CloudinaryImplemented : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Materials",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Materials");
        }
    }
}

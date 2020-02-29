using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2.Data.Migrations
{
    public partial class updateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryBatches",
                table: "DeliveryBatches");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryBatches_MaterialId",
                table: "DeliveryBatches");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Materials",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Materials",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialType",
                table: "Materials",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "SupplierId",
                table: "DeliveryBatches",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialId",
                table: "DeliveryBatches",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "DeliveryBatches",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "BatchCreatedOn",
                table: "DeliveryBatches",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "DeliveryBatches",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryBatches",
                table: "DeliveryBatches",
                columns: new[] { "MaterialId", "SupplierId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryBatches",
                table: "DeliveryBatches");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "MaterialType",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "BatchCreatedOn",
                table: "DeliveryBatches");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "DeliveryBatches");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "DeliveryBatches",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SupplierId",
                table: "DeliveryBatches",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "MaterialId",
                table: "DeliveryBatches",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryBatches",
                table: "DeliveryBatches",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBatches_MaterialId",
                table: "DeliveryBatches",
                column: "MaterialId");
        }
    }
}

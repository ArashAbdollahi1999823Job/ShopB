using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class UpdateCatalogType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 7, 12, 50, 52, 821, DateTimeKind.Local).AddTicks(8418),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 6, 18, 38, 49, 130, DateTimeKind.Local).AddTicks(2017));

            migrationBuilder.AddColumn<int>(
                name: "ParentCatalogTypeId",
                table: "CatalogTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 7, 12, 50, 52, 821, DateTimeKind.Local).AddTicks(6119),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 6, 18, 38, 49, 129, DateTimeKind.Local).AddTicks(8145));

            migrationBuilder.CreateIndex(
                name: "IX_CatalogTypes_ParentCatalogTypeId",
                table: "CatalogTypes",
                column: "ParentCatalogTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogTypes_CatalogTypes_ParentCatalogTypeId",
                table: "CatalogTypes",
                column: "ParentCatalogTypeId",
                principalTable: "CatalogTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogTypes_CatalogTypes_ParentCatalogTypeId",
                table: "CatalogTypes");

            migrationBuilder.DropIndex(
                name: "IX_CatalogTypes_ParentCatalogTypeId",
                table: "CatalogTypes");

            migrationBuilder.DropColumn(
                name: "ParentCatalogTypeId",
                table: "CatalogTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 6, 18, 38, 49, 130, DateTimeKind.Local).AddTicks(2017),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 7, 12, 50, 52, 821, DateTimeKind.Local).AddTicks(8418));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 6, 18, 38, 49, 129, DateTimeKind.Local).AddTicks(8145),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 7, 12, 50, 52, 821, DateTimeKind.Local).AddTicks(6119));
        }
    }
}

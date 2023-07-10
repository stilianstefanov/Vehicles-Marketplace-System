using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThinkElectric.Data.Migrations
{
    public partial class AddReviewsForCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Companies");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Reviews",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CompanyId",
                table: "Reviews",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Companies_CompanyId",
                table: "Reviews",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Companies_CompanyId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CompanyId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Reviews");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Companies",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

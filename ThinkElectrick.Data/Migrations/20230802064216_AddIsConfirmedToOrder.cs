using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThinkElectric.Data.Migrations
{
    public partial class AddIsConfirmedToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmedByUser",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmedByUser",
                table: "Orders");
        }
    }
}

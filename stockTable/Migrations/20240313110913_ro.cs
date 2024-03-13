using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockTable.Migrations
{
    public partial class ro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Documents_InventoryNum",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "InventoryNum",
                table: "Documents");

            migrationBuilder.AddColumn<string>(
                name: "InventoryNum",
                table: "Equipments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_InventoryNum",
                table: "Equipments",
                column: "InventoryNum",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Equipments_InventoryNum",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "InventoryNum",
                table: "Equipments");

            migrationBuilder.AddColumn<string>(
                name: "InventoryNum",
                table: "Documents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_InventoryNum",
                table: "Documents",
                column: "InventoryNum",
                unique: true);
        }
    }
}

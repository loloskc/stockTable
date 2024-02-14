using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockTable.Migrations
{
    public partial class downContractNum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractNum",
                table: "Documents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractNum",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

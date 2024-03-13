using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockTable.Migrations
{
    public partial class LowPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LowEquipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeEq = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IdDocument = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LowEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LowEquipments_Documents_IdDocument",
                        column: x => x.IdDocument,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LowEquipments_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Color", "Name" },
                values: new object[] { 4, "table-warning", "требуется ремонт" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Color", "Name" },
                values: new object[] { 5, "table-primary", "резерв" });

            migrationBuilder.CreateIndex(
                name: "IX_LowEquipments_IdDocument",
                table: "LowEquipments",
                column: "IdDocument");

            migrationBuilder.CreateIndex(
                name: "IX_LowEquipments_StatusId",
                table: "LowEquipments",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LowEquipments");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}

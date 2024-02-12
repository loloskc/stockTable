using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockTable.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CabinetNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumContract = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataContract = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Responsible = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameForBookkeeping = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeEq = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamePC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OCName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dataE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IdDocument = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipments_Documents_IdDocument",
                        column: x => x.IdDocument,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipments_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_IdDocument",
                table: "Equipments",
                column: "IdDocument");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_StatusId",
                table: "Equipments",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}

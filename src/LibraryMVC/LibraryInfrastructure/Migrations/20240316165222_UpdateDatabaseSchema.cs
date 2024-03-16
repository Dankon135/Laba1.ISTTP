using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Laboratories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Departament_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Laboratories_Departments",
                        column: x => x.Departament_ID,
                        principalTable: "Departments",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Personnel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Full_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Departament_ID = table.Column<int>(type: "int", nullable: false),
                    Laboratory_ID = table.Column<int>(type: "int", nullable: false),
                    Position_ID = table.Column<int>(type: "int", nullable: false),
                    Position_Start = table.Column<DateOnly>(type: "date", nullable: false),
                    Position_End = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Personnel_Departments",
                        column: x => x.ID,
                        principalTable: "Departments",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Personnel_Laboratories",
                        column: x => x.Laboratory_ID,
                        principalTable: "Laboratories",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Personnel_Position",
                        column: x => x.Position_ID,
                        principalTable: "Position",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Scientific_Works",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Researcher_ID = table.Column<int>(type: "int", nullable: false),
                    Client = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Client_Address = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Subordination = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Field = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Personnel_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scientific_Works", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Scientific_Works_Personnel",
                        column: x => x.Personnel_ID,
                        principalTable: "Personnel",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Researcher_Work",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Researcher_ID = table.Column<int>(type: "int", nullable: false),
                    Scientific_Work_ID = table.Column<int>(type: "int", nullable: false),
                    Contribution = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Created_At = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Researcher_Work", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Researcher_Work_Scientific_Works1",
                        column: x => x.ID,
                        principalTable: "Scientific_Works",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Laboratories_Departament_ID",
                table: "Laboratories",
                column: "Departament_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_Laboratory_ID",
                table: "Personnel",
                column: "Laboratory_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_Position_ID",
                table: "Personnel",
                column: "Position_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Scientific_Works_Personnel_ID",
                table: "Scientific_Works",
                column: "Personnel_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Researcher_Work");

            migrationBuilder.DropTable(
                name: "Scientific_Works");

            migrationBuilder.DropTable(
                name: "Personnel");

            migrationBuilder.DropTable(
                name: "Laboratories");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}

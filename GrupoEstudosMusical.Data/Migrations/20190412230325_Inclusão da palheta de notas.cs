using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class Inclusãodapalhetadenotas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PalhetaDeNotas",
                columns: table => new
                {
                    IdPalheta = table.Column<Guid>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    MatriculaID = table.Column<int>(nullable: false),
                    Nota = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PalhetaDeNotas", x => x.IdPalheta);
                    table.ForeignKey(
                        name: "FK_PalhetaDeNotas_Matriculas_MatriculaID",
                        column: x => x.MatriculaID,
                        principalTable: "Matriculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PalhetaDeNotas_MatriculaID",
                table: "PalhetaDeNotas",
                column: "MatriculaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PalhetaDeNotas");
        }
    }
}

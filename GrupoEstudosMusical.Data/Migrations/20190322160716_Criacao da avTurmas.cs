using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class CriacaodaavTurmas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvaliacoesTurmas",
                columns: table => new
                {
                    TurmaID = table.Column<int>(nullable: false),
                    AvaliacaoID = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    DataPrevista = table.Column<DateTime>(type: "date", nullable: false),
                    DataRealizacao = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacoesTurmas", x => new { x.AvaliacaoID, x.TurmaID });
                    table.ForeignKey(
                        name: "FK_AvaliacoesTurmas_Avaliacoes_AvaliacaoID",
                        column: x => x.AvaliacaoID,
                        principalTable: "Avaliacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvaliacoesTurmas_Turmas_TurmaID",
                        column: x => x.TurmaID,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacoesTurmas_TurmaID",
                table: "AvaliacoesTurmas",
                column: "TurmaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvaliacoesTurmas");
        }
    }
}

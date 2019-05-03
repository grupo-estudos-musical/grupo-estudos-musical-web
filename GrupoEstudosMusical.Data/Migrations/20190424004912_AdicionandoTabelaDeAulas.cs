using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class AdicionandoTabelaDeAulas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AulaId",
                table: "AvaliacoesDaTurma",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Conteudo = table.Column<string>(type: "varchar(300)", nullable: false),
                    TurmaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aulas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacoesDaTurma_AulaId",
                table: "AvaliacoesDaTurma",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_TurmaId",
                table: "Aulas",
                column: "TurmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AvaliacoesDaTurma_Aulas_AulaId",
                table: "AvaliacoesDaTurma",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvaliacoesDaTurma_Aulas_AulaId",
                table: "AvaliacoesDaTurma");

            migrationBuilder.DropTable(
                name: "Aulas");

            migrationBuilder.DropIndex(
                name: "IX_AvaliacoesDaTurma_AulaId",
                table: "AvaliacoesDaTurma");

            migrationBuilder.DropColumn(
                name: "AulaId",
                table: "AvaliacoesDaTurma");
        }
    }
}

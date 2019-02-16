using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class Ocorrencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ocorrencias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(70)", nullable: false),
                    DataOcorrido = table.Column<DateTime>(type: "date", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Resumo = table.Column<string>(type: "varchar(300)", nullable: false),
                    TurmaID = table.Column<int>(nullable: false),
                    NomeProfessor = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ocorrencias_Turmas_TurmaID",
                        column: x => x.TurmaID,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_TurmaID",
                table: "Ocorrencias",
                column: "TurmaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ocorrencias");
        }
    }
}

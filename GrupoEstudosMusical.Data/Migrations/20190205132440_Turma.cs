using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class Turma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Nome = table.Column<string>(type: "varchar(60)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "date", nullable: false),
                    TerminoAula = table.Column<DateTime>(type: "date", nullable: false),
                    Periodo = table.Column<int>(type: "int", nullable: false),
                    Nivel = table.Column<string>(type: "varchar(15)", nullable: false),
                    Status = table.Column<string>(type: "varchar(11)", nullable: false, defaultValue: "Ativo"),
                    QuantidadeAlunos = table.Column<int>(type: "int", nullable: false),
                    ProfessorID = table.Column<int>(nullable: false),
                    ModuloID = table.Column<int>(nullable: false),
                    Semestre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turmas_Modulos_ModuloID",
                        column: x => x.ModuloID,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turmas_Professores_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_ModuloID",
                table: "Turmas",
                column: "ModuloID");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_ProfessorID",
                table: "Turmas",
                column: "ProfessorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Turmas");
        }
    }
}

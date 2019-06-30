using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class Ajustesnarelacaodeturmascomprofessor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Professores_ProfessorID",
                table: "Turmas");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Professores_ProfessorID",
                table: "Turmas",
                column: "ProfessorID",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Professores_ProfessorID",
                table: "Turmas");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Professores_ProfessorID",
                table: "Turmas",
                column: "ProfessorID",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

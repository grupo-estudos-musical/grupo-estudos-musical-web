using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class Edicaonaestruturadaavaliacoesturmas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvaliacoesTurmas_Avaliacoes_AvaliacaoID",
                table: "AvaliacoesTurmas");

            migrationBuilder.DropForeignKey(
                name: "FK_AvaliacoesTurmas_Turmas_TurmaID",
                table: "AvaliacoesTurmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvaliacoesTurmas",
                table: "AvaliacoesTurmas");

            migrationBuilder.RenameTable(
                name: "AvaliacoesTurmas",
                newName: "AvaliacoesDaTurma");

            migrationBuilder.RenameIndex(
                name: "IX_AvaliacoesTurmas_TurmaID",
                table: "AvaliacoesDaTurma",
                newName: "IX_AvaliacoesDaTurma_TurmaID");

            migrationBuilder.AddColumn<Guid>(
                name: "IdAvaliacaoTurma",
                table: "AvaliacoesDaTurma",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvaliacoesDaTurma",
                table: "AvaliacoesDaTurma",
                column: "IdAvaliacaoTurma");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacoesDaTurma_AvaliacaoID_TurmaID",
                table: "AvaliacoesDaTurma",
                columns: new[] { "AvaliacaoID", "TurmaID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AvaliacoesDaTurma_Avaliacoes_AvaliacaoID",
                table: "AvaliacoesDaTurma",
                column: "AvaliacaoID",
                principalTable: "Avaliacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AvaliacoesDaTurma_Turmas_TurmaID",
                table: "AvaliacoesDaTurma",
                column: "TurmaID",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvaliacoesDaTurma_Avaliacoes_AvaliacaoID",
                table: "AvaliacoesDaTurma");

            migrationBuilder.DropForeignKey(
                name: "FK_AvaliacoesDaTurma_Turmas_TurmaID",
                table: "AvaliacoesDaTurma");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvaliacoesDaTurma",
                table: "AvaliacoesDaTurma");

            migrationBuilder.DropIndex(
                name: "IX_AvaliacoesDaTurma_AvaliacaoID_TurmaID",
                table: "AvaliacoesDaTurma");

            migrationBuilder.DropColumn(
                name: "IdAvaliacaoTurma",
                table: "AvaliacoesDaTurma");

            migrationBuilder.RenameTable(
                name: "AvaliacoesDaTurma",
                newName: "AvaliacoesTurmas");

            migrationBuilder.RenameIndex(
                name: "IX_AvaliacoesDaTurma_TurmaID",
                table: "AvaliacoesTurmas",
                newName: "IX_AvaliacoesTurmas_TurmaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvaliacoesTurmas",
                table: "AvaliacoesTurmas",
                columns: new[] { "AvaliacaoID", "TurmaID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AvaliacoesTurmas_Avaliacoes_AvaliacaoID",
                table: "AvaliacoesTurmas",
                column: "AvaliacaoID",
                principalTable: "Avaliacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AvaliacoesTurmas_Turmas_TurmaID",
                table: "AvaliacoesTurmas",
                column: "TurmaID",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

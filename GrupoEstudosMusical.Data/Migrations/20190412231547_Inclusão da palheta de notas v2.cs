using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class Inclusãodapalhetadenotasv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AvaliacaoID",
                table: "PalhetaDeNotas",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PalhetaDeNotas_AvaliacaoID",
                table: "PalhetaDeNotas",
                column: "AvaliacaoID");

            migrationBuilder.AddForeignKey(
                name: "FK_PalhetaDeNotas_AvaliacoesDaTurma_AvaliacaoID",
                table: "PalhetaDeNotas",
                column: "AvaliacaoID",
                principalTable: "AvaliacoesDaTurma",
                principalColumn: "IdAvaliacaoTurma",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PalhetaDeNotas_AvaliacoesDaTurma_AvaliacaoID",
                table: "PalhetaDeNotas");

            migrationBuilder.DropIndex(
                name: "IX_PalhetaDeNotas_AvaliacaoID",
                table: "PalhetaDeNotas");

            migrationBuilder.DropColumn(
                name: "AvaliacaoID",
                table: "PalhetaDeNotas");
        }
    }
}

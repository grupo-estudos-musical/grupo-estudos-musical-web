using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class Vinculandoempréstimoaousuário : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "InstrumentoDoAluno",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentoDoAluno_UsuarioId",
                table: "InstrumentoDoAluno",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstrumentoDoAluno_Usuarios_UsuarioId",
                table: "InstrumentoDoAluno",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstrumentoDoAluno_Usuarios_UsuarioId",
                table: "InstrumentoDoAluno");

            migrationBuilder.DropIndex(
                name: "IX_InstrumentoDoAluno_UsuarioId",
                table: "InstrumentoDoAluno");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "InstrumentoDoAluno");
        }
    }
}

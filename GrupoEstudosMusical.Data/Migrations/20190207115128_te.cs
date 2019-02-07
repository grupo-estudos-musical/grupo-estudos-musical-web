using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class te : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Turmas",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_Nome_Periodo_Semestre",
                table: "Turmas",
                columns: new[] { "Nome", "Periodo", "Semestre" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Turmas_Nome_Periodo_Semestre",
                table: "Turmas");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Turmas",
                type: "varchar(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);
        }
    }
}

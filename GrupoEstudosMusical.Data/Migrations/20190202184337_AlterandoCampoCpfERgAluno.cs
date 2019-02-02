using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class AlterandoCampoCpfERgAluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rg",
                table: "Alunos",
                type: "varchar(12)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Alunos",
                type: "varchar(14)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rg",
                table: "Alunos",
                type: "varchar(9)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Alunos",
                type: "varchar(11)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(14)",
                oldNullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class Alteracao_Turma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Turmas",
                type: "varchar(11)",
                nullable: false,
                defaultValue: "Ativo",
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldDefaultValueSql: "Ativo");

            migrationBuilder.AlterColumn<int>(
                name: "QuantidadeAlunos",
                table: "Turmas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Turmas",
                type: "varchar(11)",
                nullable: false,
                defaultValueSql: "Ativo",
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldDefaultValue: "Ativo");

            migrationBuilder.AlterColumn<int>(
                name: "QuantidadeAlunos",
                table: "Turmas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }
    }
}

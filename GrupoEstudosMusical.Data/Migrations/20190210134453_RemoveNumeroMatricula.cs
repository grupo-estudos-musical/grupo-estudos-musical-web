using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class RemoveNumeroMatricula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroMatricula",
                table: "Matriculas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumeroMatricula",
                table: "Matriculas",
                nullable: false,
                defaultValue: 0);
        }
    }
}

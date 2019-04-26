using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class CriaçãodosmigrationsdefabricanteeinstrumentoV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cor",
                table: "Instrumentos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cor",
                table: "Instrumentos",
                type: "varchar(10)",
                nullable: true);
        }
    }
}

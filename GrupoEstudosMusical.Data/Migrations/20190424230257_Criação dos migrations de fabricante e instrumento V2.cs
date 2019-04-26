using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class CriaçãodosmigrationsdefabricanteeinstrumentoV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instrumentos_Fabricantes_FabricanteID",
                table: "Instrumentos");

            migrationBuilder.DropIndex(
                name: "IX_Instrumentos_FabricanteID",
                table: "Instrumentos");

            migrationBuilder.DropColumn(
                name: "AnoDeFabricacao",
                table: "Instrumentos");

            migrationBuilder.DropColumn(
                name: "FabricanteID",
                table: "Instrumentos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AnoDeFabricacao",
                table: "Instrumentos",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FabricanteID",
                table: "Instrumentos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Instrumentos_FabricanteID",
                table: "Instrumentos",
                column: "FabricanteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instrumentos_Fabricantes_FabricanteID",
                table: "Instrumentos",
                column: "FabricanteID",
                principalTable: "Fabricantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

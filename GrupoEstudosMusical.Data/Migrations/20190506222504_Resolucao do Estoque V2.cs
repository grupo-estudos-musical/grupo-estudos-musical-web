using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class ResolucaodoEstoqueV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AnoDeFabricacaoInstrumento",
                table: "InstrumentoDoAluno",
                type: "varchar(4)",
                nullable: false,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AnoDeFabricacaoInstrumento",
                table: "InstrumentoDoAluno",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(4)");
        }
    }
}

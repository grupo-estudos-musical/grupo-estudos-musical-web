using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class Remocaoderequiredparadataprevistanasavaliacoesdaturmas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataRealizacao",
                table: "AvaliacoesTurmas",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataRealizacao",
                table: "AvaliacoesTurmas",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class Inclusãodamodelassociativaentreinstrumentoealunoefabricante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstrumentoDoAluno",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    AnoDeFabricacaoInstrumento = table.Column<DateTime>(nullable: false),
                    DataEmprestimo = table.Column<DateTime>(type: "date", nullable: false),
                    InstrumentoID = table.Column<Guid>(nullable: false),
                    FabricanteID = table.Column<Guid>(nullable: false),
                    Cor = table.Column<string>(type: "varchar(10)", nullable: false),
                    AlunoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentoDoAluno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentoDoAluno_Alunos_AlunoID",
                        column: x => x.AlunoID,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstrumentoDoAluno_Fabricantes_FabricanteID",
                        column: x => x.FabricanteID,
                        principalTable: "Fabricantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstrumentoDoAluno_Instrumentos_InstrumentoID",
                        column: x => x.InstrumentoID,
                        principalTable: "Instrumentos",
                        principalColumn: "IntrumentoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentoDoAluno_AlunoID",
                table: "InstrumentoDoAluno",
                column: "AlunoID");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentoDoAluno_FabricanteID",
                table: "InstrumentoDoAluno",
                column: "FabricanteID");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentoDoAluno_InstrumentoID_FabricanteID_AlunoID",
                table: "InstrumentoDoAluno",
                columns: new[] { "InstrumentoID", "FabricanteID", "AlunoID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstrumentoDoAluno");
        }
    }
}

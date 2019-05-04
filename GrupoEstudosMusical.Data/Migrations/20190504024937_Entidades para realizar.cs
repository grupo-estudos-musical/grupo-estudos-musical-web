using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrupoEstudosMusical.Data.Migrations
{
    public partial class Entidadespararealizar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(14)", nullable: true),
                    Celular = table.Column<string>(type: "varchar(15)", nullable: true),
                    Rg = table.Column<string>(type: "varchar(12)", nullable: true),
                    Cpf = table.Column<string>(type: "varchar(14)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    TipoResponsavel = table.Column<string>(type: "varchar(50)", nullable: false),
                    NomeResponsavel = table.Column<string>(type: "varchar(50)", nullable: true),
                    Cep = table.Column<string>(type: "varchar(9)", nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(180)", nullable: false),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: false),
                    Bairro = table.Column<string>(type: "varchar(60)", nullable: false),
                    Complemento = table.Column<string>(type: "varchar(50)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(60)", nullable: false),
                    Uf = table.Column<string>(type: "varchar(2)", nullable: false),
                    ImagemUrl = table.Column<string>(type: "varchar(300)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    NotaMaxima = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fabricantes",
                columns: table => new
                {
                    IdFabricante = table.Column<Guid>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fabricantes", x => x.IdFabricante);
                });

            migrationBuilder.CreateTable(
                name: "Instrumentos",
                columns: table => new
                {
                    IntrumentoID = table.Column<Guid>(nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Nome = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrumentos", x => x.IntrumentoID);
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Nome = table.Column<string>(type: "varchar(60)", nullable: false),
                    Observacoes = table.Column<string>(type: "varchar(300)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(14)", nullable: true),
                    Celular = table.Column<string>(type: "varchar(15)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Cep = table.Column<string>(type: "varchar(9)", nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(180)", nullable: false),
                    Numero = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(type: "varchar(50)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(60)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(60)", nullable: false),
                    Uf = table.Column<string>(type: "varchar(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    InventarioID = table.Column<Guid>(nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    QuantidadeDisponivel = table.Column<int>(nullable: false),
                    EstoqueMinimo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario", x => x.InventarioID);
                    table.ForeignKey(
                        name: "FK_Inventario_Instrumentos_InventarioID",
                        column: x => x.InventarioID,
                        principalTable: "Instrumentos",
                        principalColumn: "IntrumentoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Nome = table.Column<string>(type: "varchar(60)", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "date", nullable: false),
                    TerminoAula = table.Column<DateTime>(type: "date", nullable: false),
                    Periodo = table.Column<int>(type: "int", nullable: false),
                    Nivel = table.Column<string>(type: "varchar(15)", nullable: false),
                    Status = table.Column<string>(type: "varchar(11)", nullable: false, defaultValue: "Ativo"),
                    QuantidadeAlunos = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ProfessorID = table.Column<int>(nullable: false),
                    ModuloID = table.Column<int>(nullable: false),
                    Semestre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turmas_Modulos_ModuloID",
                        column: x => x.ModuloID,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turmas_Professores_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentoDoAluno",
                columns: table => new
                {
                    IdInstrumentoDoAluno = table.Column<Guid>(nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    AnoDeFabricacaoInstrumento = table.Column<DateTime>(nullable: false),
                    DataEmprestimo = table.Column<DateTime>(type: "date", nullable: false),
                    InventarioID = table.Column<Guid>(nullable: false),
                    FabricanteID = table.Column<Guid>(nullable: false),
                    Cor = table.Column<string>(type: "varchar(10)", nullable: false),
                    AlunoID = table.Column<int>(nullable: false),
                    Status = table.Column<string>(type: "varchar(11)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentoDoAluno", x => x.IdInstrumentoDoAluno);
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
                        principalColumn: "IdFabricante",
                        onDelete: ReferentialAction.Cascade);
                   
                    table.ForeignKey(
                        name: "FK_InstrumentoDoAluno_Inventario_InventarioID",
                        column: x => x.InventarioID,
                        principalTable: "Inventario",
                        principalColumn: "InventarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Conteudo = table.Column<string>(type: "varchar(300)", nullable: false),
                    TurmaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aulas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chamadas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Observacao = table.Column<string>(type: "varchar(200)", nullable: true),
                    IdTurma = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chamadas_Turmas_IdTurma",
                        column: x => x.IdTurma,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
                    Cpf = table.Column<bool>(type: "bit", nullable: false),
                    Rg = table.Column<bool>(type: "bit", nullable: false),
                    ComprovanteResidencia = table.Column<bool>(type: "bit", nullable: false),
                    Pendente = table.Column<bool>(type: "bit", nullable: false),
                    Media = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matriculas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matriculas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ocorrencias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(70)", nullable: false),
                    DataOcorrido = table.Column<DateTime>(type: "date", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(30)", nullable: false),
                    Resumo = table.Column<string>(type: "varchar(300)", nullable: false),
                    TurmaID = table.Column<int>(nullable: false),
                    AlunoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ocorrencias_Alunos_AlunoID",
                        column: x => x.AlunoID,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ocorrencias_Turmas_TurmaID",
                        column: x => x.TurmaID,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvaliacoesDaTurma",
                columns: table => new
                {
                    IdAvaliacaoTurma = table.Column<Guid>(nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    TurmaID = table.Column<int>(nullable: false),
                    AvaliacaoID = table.Column<int>(nullable: false),
                    DataPrevista = table.Column<DateTime>(type: "date", nullable: false),
                    DataRealizacao = table.Column<DateTime>(nullable: false),
                    AulaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacoesDaTurma", x => x.IdAvaliacaoTurma);
                    table.ForeignKey(
                        name: "FK_AvaliacoesDaTurma_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalTable: "Aulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AvaliacoesDaTurma_Avaliacoes_AvaliacaoID",
                        column: x => x.AvaliacaoID,
                        principalTable: "Avaliacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvaliacoesDaTurma_Turmas_TurmaID",
                        column: x => x.TurmaID,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Frequencias",
                columns: table => new
                {
                    IdChamada = table.Column<int>(nullable: false),
                    IdAluno = table.Column<int>(nullable: false),
                    Presenca = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequencias", x => new { x.IdChamada, x.IdAluno });
                    table.ForeignKey(
                        name: "FK_Frequencias_Alunos_IdAluno",
                        column: x => x.IdAluno,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Frequencias_Chamadas_IdChamada",
                        column: x => x.IdChamada,
                        principalTable: "Chamadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PalhetaDeNotas",
                columns: table => new
                {
                    IdPalheta = table.Column<Guid>(nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    AvaliacaoID = table.Column<Guid>(nullable: false),
                    MatriculaID = table.Column<int>(nullable: false),
                    Nota = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PalhetaDeNotas", x => x.IdPalheta);
                    table.ForeignKey(
                        name: "FK_PalhetaDeNotas_AvaliacoesDaTurma_AvaliacaoID",
                        column: x => x.AvaliacaoID,
                        principalTable: "AvaliacoesDaTurma",
                        principalColumn: "IdAvaliacaoTurma",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PalhetaDeNotas_Matriculas_MatriculaID",
                        column: x => x.MatriculaID,
                        principalTable: "Matriculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_Cpf",
                table: "Alunos",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_TurmaId",
                table: "Aulas",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_Nome",
                table: "Avaliacoes",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacoesDaTurma_AulaId",
                table: "AvaliacoesDaTurma",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacoesDaTurma_TurmaID",
                table: "AvaliacoesDaTurma",
                column: "TurmaID");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacoesDaTurma_AvaliacaoID_TurmaID",
                table: "AvaliacoesDaTurma",
                columns: new[] { "AvaliacaoID", "TurmaID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chamadas_IdTurma",
                table: "Chamadas",
                column: "IdTurma");

            migrationBuilder.CreateIndex(
                name: "IX_Frequencias_IdAluno",
                table: "Frequencias",
                column: "IdAluno");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentoDoAluno_AlunoID",
                table: "InstrumentoDoAluno",
                column: "AlunoID");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentoDoAluno_FabricanteID",
                table: "InstrumentoDoAluno",
                column: "FabricanteID");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentoDoAluno_InventarioID_FabricanteID_AlunoID",
                table: "InstrumentoDoAluno",
                columns: new[] { "InventarioID", "FabricanteID", "AlunoID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_AlunoId",
                table: "Matriculas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_TurmaId",
                table: "Matriculas",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_AlunoID",
                table: "Ocorrencias",
                column: "AlunoID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_TurmaID",
                table: "Ocorrencias",
                column: "TurmaID");

            migrationBuilder.CreateIndex(
                name: "IX_PalhetaDeNotas_AvaliacaoID",
                table: "PalhetaDeNotas",
                column: "AvaliacaoID");

            migrationBuilder.CreateIndex(
                name: "IX_PalhetaDeNotas_MatriculaID",
                table: "PalhetaDeNotas",
                column: "MatriculaID");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_ModuloID",
                table: "Turmas",
                column: "ModuloID");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_ProfessorID",
                table: "Turmas",
                column: "ProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_Nome_Periodo_Semestre",
                table: "Turmas",
                columns: new[] { "Nome", "Periodo", "Semestre" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Frequencias");

            migrationBuilder.DropTable(
                name: "InstrumentoDoAluno");

            migrationBuilder.DropTable(
                name: "Ocorrencias");

            migrationBuilder.DropTable(
                name: "PalhetaDeNotas");

            migrationBuilder.DropTable(
                name: "Chamadas");

            migrationBuilder.DropTable(
                name: "Fabricantes");

            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "AvaliacoesDaTurma");

            migrationBuilder.DropTable(
                name: "Matriculas");

            migrationBuilder.DropTable(
                name: "Instrumentos");

            migrationBuilder.DropTable(
                name: "Aulas");

            migrationBuilder.DropTable(
                name: "Avaliacoes");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "Professores");
        }
    }
}

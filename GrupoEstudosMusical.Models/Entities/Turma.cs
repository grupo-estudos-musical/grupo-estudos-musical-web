using System;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Entities
{
    public class Turma:BaseEntity
    {
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime TerminoAula { get; set; }
        public int Periodo { get; set; }
        public string Nivel { get; set; }
        public virtual Professor Professor { get; set; }
        public string Status { get; set; }
        public int QuantidadeAlunos { get; set; }
        public int ProfessorID { get; set; }
        public int ModuloID { get; set; }
        public virtual Modulo Modulo { get; set; }
        public int Semestre { get; set; }
        public List<Matricula> Matriculas { get; set; }
        public List<Ocorrencia> Ocorrencias { get; set; }

        public List<AvaliacaoTurma> AvaliacoesTurmas { get; set; }
        public List<Chamada> Chamadas { get; set; }

        public bool VerificarQuantidadeDeAlunosMatriculados() => Matriculas.Count < QuantidadeAlunos;
    }
}

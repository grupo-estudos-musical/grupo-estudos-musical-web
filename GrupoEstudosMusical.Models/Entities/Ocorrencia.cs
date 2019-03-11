

using System;
using System.ComponentModel;

namespace GrupoEstudosMusical.Models.Entities
{
    public class Ocorrencia:BaseEntity
    {
        public string Titulo { get; set; }
        public DateTime DataOcorrido { get; set; }
        public string Tipo { get; set; }
        public string Resumo { get; set; }
        public virtual Turma Turma { get; set; }

        [DisplayName("Turma")]
        public int TurmaID { get; set; }

        [DisplayName("Aluno")]
        public int AlunoID { get; set; }
        public virtual Aluno Aluno { get; set; }
    }
}

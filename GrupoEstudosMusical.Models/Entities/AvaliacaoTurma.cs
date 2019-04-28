using System;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Entities
{
    public class AvaliacaoTurma:BaseEntity
    {
        
        public Guid IdAvaliacaoTurma { get; set; }
        public virtual Turma Turma { get; set; }
        public int TurmaID { get; private set; }
        public virtual Avaliacao Avaliacao { get; set; }
        public int AvaliacaoID { get;  set; }
        public DateTime DataPrevista { get;  set; }
        public DateTime DataRealizacao { get;  set; }
        public int? AulaId { get; set; }
        public Aula Aula { get; set; }
        public List<PalhetaDeNota> PalhetaDeNotas { get; set; }
    }
}

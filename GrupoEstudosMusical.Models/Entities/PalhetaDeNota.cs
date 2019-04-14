
using System;

namespace GrupoEstudosMusical.Models.Entities
{
    public class PalhetaDeNota:BaseEntity
    {
        public PalhetaDeNota()
        {
            IdPalheta = Guid.NewGuid();
        }
        public Guid IdPalheta { get; protected set; }
        public Guid AvaliacaoID { get; set; }
        public int MatriculaID { get;  set; }
        public double? Nota { get; set; }
        public AvaliacaoTurma AvaliacaoTurma { get; set; }
        public Matricula Matricula { get; set; }
    }
}

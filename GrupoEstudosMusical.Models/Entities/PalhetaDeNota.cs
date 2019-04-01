
using System;

namespace GrupoEstudosMusical.Models.Entities
{
    public class PalhetaDeNota:BaseEntity
    {
        public PalhetaDeNota(int matricula, int turmaID, int avaliacaoID, double nota, Guid idPalheta)
        {
           
            TurmaID = turmaID;
            AvaliacaoID = avaliacaoID;
            Nota = nota;
            IdPalheta = idPalheta;
        }
        public Guid IdPalheta { get; set; }
        public int MatriculaID { get; protected set; }
        public int TurmaID { get; protected set; }
        public int AvaliacaoID { get; protected set; }
        public double? Nota { get; protected set; }

    }
}

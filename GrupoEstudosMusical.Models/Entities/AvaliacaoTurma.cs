

using System;

namespace GrupoEstudosMusical.Models.Entities
{
    public class AvaliacaoTurma:BaseEntity
    {
        public AvaliacaoTurma(int TurmaID, int AvaliacaoID, DateTime DataPrevista, DateTime DataRealizacao)
        {
            this.TurmaID = TurmaID;
            this.AvaliacaoID = AvaliacaoID;
            this.DataPrevista = DataPrevista;
            this.DataRealizacao = DataRealizacao;

        }
        public virtual Turma Turma { get; private set; }
        public int TurmaID { get; private set; }
        public virtual Avaliacao Avaliacao { get; private set; }
        public int AvaliacaoID { get; private set; }
        public DateTime DataPrevista { get; private set; }
        public DateTime DataRealizacao { get; private set; }
    }
}

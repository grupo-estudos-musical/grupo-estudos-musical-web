using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Entities
{
    public class Aula : BaseEntity
    {
        public string Conteudo { get; set; }
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
        public List<AvaliacaoTurma> AvaliacaosTurma { get; set; } = new List<AvaliacaoTurma>();
    }
}

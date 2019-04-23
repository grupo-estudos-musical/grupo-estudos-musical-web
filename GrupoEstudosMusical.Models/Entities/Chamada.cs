using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Entities
{
    public class Chamada : BaseEntity
    {
        public string Observacao { get; set; }
        public int IdTurma { get; set; }
        public Turma Turma { get; set; }
        public List<Frequencia> Frequencias { get; set; }
    }
}



using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Entities
{
    public class Modulo:BaseEntity
    {
        public string Nome { get; set; }
        public string Observacoes { get; set; }
        public ICollection<Turma> Turmas { get; set; }
    }
}

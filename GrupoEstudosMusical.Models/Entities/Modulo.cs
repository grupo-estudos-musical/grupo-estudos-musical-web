

using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Entities
{
    public class Modulo:BaseEntity
    {
        public Modulo()
        {
            Turmas = new List<Turma>();
        }
        public string Nome { get; set; }
        public string Observacoes { get; set; }

        public virtual List<Turma> Turmas { get; set; }

    }
}

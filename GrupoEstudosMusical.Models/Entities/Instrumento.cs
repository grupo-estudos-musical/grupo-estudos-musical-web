

using System;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Entities
{
    public class Instrumento : BaseEntity
    {
        public Instrumento()
        {
            this.IntrumentoID = Guid.NewGuid();
        }
        public Guid IntrumentoID {get;  private set; }
        public string Nome { get; set; }
        public List<InstrumentoDoAluno> Instrumentos { get; set; }
        public class Fabricante:BaseEntity
        {
            public List<InstrumentoDoAluno> Instrumentos { get; set; }
            public new Guid Id { get; set; }
            public string Nome { get; set; }
        }

    }
}



using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public virtual Inventario Inventario { get; set; }

        public class Fabricante:BaseEntity
        {
            public Fabricante()
            {
                    IdFabricante = Guid.NewGuid();
            }
            public List<InstrumentoDoAluno> InstrumentosDoAluno { get; set; }
            [Key]
            public Guid IdFabricante { get; set; }
            public string Nome { get; set; }
        }


    }
}

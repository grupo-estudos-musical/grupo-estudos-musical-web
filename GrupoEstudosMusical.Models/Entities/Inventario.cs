
using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models
{
    public class Inventario : BaseEntity
    {
        public Guid InventarioID { get; set; }

        public int QuantidadeDisponivel { get; set; }
        public int EstoqueMinimo { get; set; }
        public List<InstrumentoDoAluno> InstrumentosDoAluno { get; set; }
        public virtual Instrumento Instrumento { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrupoEstudosMusical.MVC.Models
{
    public class FabricanteVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<InstrumentoVM> Instrumentos { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
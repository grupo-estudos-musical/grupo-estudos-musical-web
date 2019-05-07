using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;


namespace GrupoEstudosMusical.MVC.Models
{
    public class FabricanteVM
    {
        
        public Guid Id{ get; set; }
        public string Nome { get; set; }
        public List<InstrumentoDoAluno> InstrumentosDoAluno { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace GrupoEstudosMusical.MVC.Models
{
    public class InstrumentoVM
    {
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public Guid IntrumentoID { get; private set; }
        public string Nome { get; set; }
       
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace GrupoEstudosMusical.MVC.Models
{
    public class InstrumentoVM
    {
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public Guid IntrumentoID { get; set; }
        public string Nome { get; set; }
       
    }
}
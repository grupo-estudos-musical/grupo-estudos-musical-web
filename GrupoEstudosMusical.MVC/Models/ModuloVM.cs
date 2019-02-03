using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GrupoEstudosMusical.MVC.Models
{
    public class ModuloVM
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }

        [DisplayName("Observações")]
        [Required]
        public string Observacoes { get; set; }

        [DisplayName("Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
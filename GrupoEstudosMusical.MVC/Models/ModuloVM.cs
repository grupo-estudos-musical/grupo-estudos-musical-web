using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GrupoEstudosMusical.MVC.Models
{
    public class ModuloVM
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }

        [DisplayName("Observações")]
        public string Observacoes { get; set; }

        [DisplayName("Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
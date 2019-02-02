using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GrupoEstudosMusical.MVC.Models
{
    public class AlunoVM
    {
        public int Id { get; set; }        
        public string Nome { get; set; }

        [DisplayName("Nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; } = DateTime.Now;
 
        public string Telefone { get; set; }

        public string Celular { get; set; }

        [DisplayName("RG")]
        public string Rg { get; set; }

        [DisplayName("CPF")]
        public string Cpf { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        public string Cep { get; set; }

        [DisplayName("Endereço")]
        public string Endereco { get; set; }

        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }

        [DisplayName("UF")]
        public string Uf { get; set; } = "SP";        
    }
}
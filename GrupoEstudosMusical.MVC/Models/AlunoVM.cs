using GrupoEstudosMusical.Models.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

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

        [DisplayName("Responsável")]
        public string NomeResponsavel { get; set; }

        [DisplayName("Parentesco")]
        public TipoResponsavelEnum TipoResponsavel { get; set; }

        public string Cep { get; set; }

        [DisplayName("Logradouro")]
        public string Logradouro { get; set; }

        [DisplayName("Número")]
        public string Numero { get; set; }

        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }

        [DisplayName("UF")]
        public string Uf { get; set; } = "SP";

        [DisplayName("Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Upload)]
        [DisplayName("Imagem")]
        public HttpPostedFileBase ImagemUpload { get; set; }

        public string ImagemUrl { get; set; }
    }
}
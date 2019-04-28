

using GrupoEstudosMusical.Models.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static GrupoEstudosMusical.Models.Entities.Instrumento;

namespace GrupoEstudosMusical.MVC.Models
{
    public class InstrumentoDoAlunoVM
    {
        public Guid Id { get; set; }
        [DisplayName("Ano de Fabricação")]
        public DateTime AnoDeFabricacaoInstrumento { get; set; }
        [DisplayName("Data do Empréstimo")]
        public DateTime DataEmprestimo { get; set; } = DateTime.Now;
        public Instrumento Instrumento { get; set; }

        [DisplayName("Instrumento")]
        public Guid InstrumentoID { get; set; }
        
        public Fabricante Fabricante { get; set; }
        [DisplayName("Fabricante")]
        public Guid FabricanteID { get; set; }
        public string Cor { get; set; }
        public AlunoVM Aluno { get; set; }

        [DisplayName("Aluno")]
        public int AlunoID { get; set; }

    }
}


using GrupoEstudosMusical.Models;

using System;
using System.ComponentModel;

using static GrupoEstudosMusical.Models.Entities.Instrumento;

namespace GrupoEstudosMusical.MVC.Models
{
    public class InstrumentoDoAlunoVM
    {

        public Guid IdInstrumentoDoAluno { get; private set; }
        [DisplayName("Ano de Fabricação")]
        public DateTime AnoDeFabricacaoInstrumento { get; set; }
        [DisplayName("Data do Empréstimo")]
        public DateTime DataEmprestimo { get; set; } = DateTime.Now;
        public Inventario Inventario { get; set; }

        [DisplayName("Instrumento")]
        public Guid InventarioID { get; set; }
        
        public Fabricante Fabricante { get; set; }
        [DisplayName("Fabricante")]
        public Guid FabricanteID { get; set; }
        public string Cor { get; set; }
        public AlunoVM Aluno { get; set; }

        [DisplayName("Aluno")]
        public int AlunoID { get; set; }

        public string NomeInstrumentoAluno { get; set; }

    }
}
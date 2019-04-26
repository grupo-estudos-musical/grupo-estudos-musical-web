using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GrupoEstudosMusical.Models.Entities.Instrumento;

namespace GrupoEstudosMusical.Models.Entities
{
    public class InstrumentoDoAluno:BaseEntity
    {
        public InstrumentoDoAluno()
        {
            Id = Guid.NewGuid();
        }
        public new Guid Id { get; set; }
        public DateTime AnoDeFabricacaoInstrumento { get; set; }
        public DateTime DataEmprestimo { get; set; } 
        public Instrumento Instrumento { get; set; }
        public Guid InstrumentoID { get; set; }
        public Fabricante Fabricante { get; set; }
        public Guid FabricanteID { get; set; }
        public string Cor { get; set; }
        public Aluno Aluno { get; set; }
        public int AlunoID { get; set; }

    }
}

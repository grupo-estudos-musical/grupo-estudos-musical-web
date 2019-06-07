

using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Entities.Relatorios
{
    public class DetalhesDoEmprestimo
    {
        public List<InstrumentoDoAluno> Instrumentos { get; set; }
        public Aluno Aluno { get; set; }
    }
}

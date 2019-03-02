using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesOcorrencia:IBussinesGeneric<Ocorrencia>
    {
        List<Ocorrencia> ObterOcorrenciasPorAluno(int AlunoId);
    }
}

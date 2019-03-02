
using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryOcorrencia:IRepositoryGeneric<Ocorrencia>
    {
        List<Ocorrencia> ObterOcorrenciasPorAluno(int AlunoId);
    }
}

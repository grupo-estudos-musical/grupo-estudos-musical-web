using System.Collections.Generic;
using System.Linq;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryOcorrencia : RepositoryGeneric<Ocorrencia>, IRepositoryOcorrencia
    {
        public List<Ocorrencia> ObterOcorrenciasPorAluno(int AlunoId)
        {
            return Context.Ocorrencias.Where(o => o.AlunoID == AlunoId).ToList();
        }
    }
}

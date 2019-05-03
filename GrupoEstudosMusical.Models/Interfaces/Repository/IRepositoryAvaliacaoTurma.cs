using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryAvaliacaoTurma:IRepositoryGeneric<AvaliacaoTurma>
    {
        AvaliacaoTurma ObterPorIds(int turma, int avaliacao);
        List<AvaliacaoTurma> ObterPorTurma(int turma);
        AvaliacaoTurma ObterPorId(Guid AvaliacaoTurmaID);
        Task<List<AvaliacaoTurma>> ObterPorAula(int idAula);
    }
}

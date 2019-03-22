

using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryAvaliacaoTurma:IRepositoryGeneric<AvaliacaoTurma>
    {
        AvaliacaoTurma ObterPorIds(int turma, int avaliacao);
        List<AvaliacaoTurma> ObterPelaTurma(int turma);
    }
}

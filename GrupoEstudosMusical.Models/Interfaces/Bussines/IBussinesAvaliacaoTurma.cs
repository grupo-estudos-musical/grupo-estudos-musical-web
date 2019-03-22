using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesAvaliacaoTurma:IBussinesGeneric<AvaliacaoTurma>
    {
        AvaliacaoTurma ObterPorIds(int turma, int avaliacao);
        List<AvaliacaoTurma> ObterPelaTurma(int turma);
    }
}

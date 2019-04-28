
using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryPalhetaDeNotas: IRepositoryGeneric<PalhetaDeNota>
    {
        IList<PalhetaDeNota> ObterPalhetasPorAvaliacaoEhTurma(Guid avaliacaoID, int turmaID);
        PalhetaDeNota ObterPorId(Guid id);
        List<PalhetaDeNota> ObterPalhetasPorMatricula(int matricula);
    }
}

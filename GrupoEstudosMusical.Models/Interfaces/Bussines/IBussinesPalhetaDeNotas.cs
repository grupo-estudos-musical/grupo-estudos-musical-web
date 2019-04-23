using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesPalhetaDeNotas:IBussinesGeneric<PalhetaDeNota>
    {
        Task AdicionarTodasAvaliacoesDaTurmaAoALuno(List<AvaliacaoTurma> avaliacoesTurma, int matriculaId);
        IList<PalhetaDeNota> ObterPalhetasPorAvaliacaoEhTurma(Guid avaliacaoID, int turmaID);
        PalhetaDeNota ObterPorId(Guid id);
        
    }
}

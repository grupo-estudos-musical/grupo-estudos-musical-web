using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesPalhetaDeNotas:IBussinesGeneric<PalhetaDeNota>
    {
        void AdicionarTodasAvaliacoesDaTurmaAoALuno(List<AvaliacaoTurma> avaliacoesTurma, int matriculaId);
    }
}

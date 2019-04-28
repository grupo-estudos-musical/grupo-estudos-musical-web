using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesAula : IBussinesGeneric<Aula>
    {
        Task InserirAsync(Aula aula, List<AvaliacaoTurma> avaliacoesTurma);
        Task<List<Aula>> ObterPorTurma(int idTurma);
    }
}

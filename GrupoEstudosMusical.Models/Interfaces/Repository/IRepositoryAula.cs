using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryAula : IRepositoryGeneric<Aula>
    {
        Task<List<Aula>> ObterPorTurma(int idTurma);
    }
}

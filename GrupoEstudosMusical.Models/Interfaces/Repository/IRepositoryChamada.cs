using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryChamada : IRepositoryGeneric<Chamada>
    {
        Task<List<Chamada>> ObterChamadasPorTurmaAsync(int idTurma);
    }
}

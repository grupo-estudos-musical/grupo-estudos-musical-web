using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesChamada : IBussinesGeneric<Chamada>
    {
        Task<List<Chamada>> ObterChamadasPorTurmaAsync(int idTurma);
    }
}

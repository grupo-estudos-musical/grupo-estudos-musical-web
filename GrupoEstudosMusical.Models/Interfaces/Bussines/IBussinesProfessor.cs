using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesProfessor : IBussinesGeneric<Professor>
    {
        Task<string> InserirAsync(Professor professor, Usuario usuario);
        Task<List<Professor>> ObterTodosPorUsuario(int idUsuario);
    }
}

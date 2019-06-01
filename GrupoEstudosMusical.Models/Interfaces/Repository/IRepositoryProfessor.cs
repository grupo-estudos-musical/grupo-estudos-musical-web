using GrupoEstudosMusical.Models.Entities;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryProfessor : IRepositoryGeneric<Professor>
    {
        Task<Professor> ObterPorEmail(string email);
    }
}

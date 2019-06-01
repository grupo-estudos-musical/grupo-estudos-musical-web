using GrupoEstudosMusical.Models.Entities;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryUsuario : IRepositoryGeneric<Usuario>
    {
        Task<Usuario> ObterPorSenhaAsync(string senha);
        Task<Usuario> AutenticarAsync(string email, string senha);
    }
}

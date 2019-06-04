using GrupoEstudosMusical.Models.Entities;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesUsuario : IBussinesGeneric<Usuario>
    {
        Task<Usuario> AutenticarAsync(string email, string senha);
        Task<string> Inserir(Usuario usuario);
        Task AlterarSenha(int idUsuario, string senha);
    }
}

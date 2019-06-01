using GrupoEstudosMusical.Models.Entities;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesAluno : IBussinesGeneric<Aluno>
    {
        Task<string> InserirAsync(Aluno aluno, Usuario usuario);
        Task<Aluno> ObterPorUsuario(int idUsuario);
    }
}

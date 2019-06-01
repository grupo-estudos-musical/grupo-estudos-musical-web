using GrupoEstudosMusical.Models.Entities;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryAluno : IRepositoryGeneric<Aluno>
    {
        Aluno ObterPorCpf(string cpf);
        Aluno ObterPorEmail(string email);

        Task<Aluno> ObterPorUsuario(int idUsuario);
    }
}

using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System.Linq;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryAluno : RepositoryGeneric<Aluno>, IRepositoryAluno
    {
        public Aluno ObterPorCpf(string cpf)
        {
            return DbSet.FirstOrDefault(a => a.Cpf == cpf);
        }
    }
}

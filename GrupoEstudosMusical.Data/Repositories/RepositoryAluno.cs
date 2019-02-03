using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryAluno : RepositoryGeneric<Aluno>, IRepositoryAluno
    {
        public Aluno ObterPorCpf(string cpf)
        {
            var aluno = DbSet.FirstOrDefault(a => a.Cpf != null && a.Cpf == cpf);
            if (aluno != null)
                Context.Entry(aluno).State = EntityState.Detached;
            return aluno;
        }
    }
}

using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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

        public Aluno ObterPorEmail(string email)
        {
            var aluno = DbSet.FirstOrDefault(a => a.Email == email);
            if (aluno != null)
                Context.Entry(aluno).State = EntityState.Detached;
            return aluno;
        }

        public async Task<Aluno> ObterPorUsuario(int idUsuario)
        {
            return await DbSet.Where(u => u.UsuarioId == idUsuario).FirstOrDefaultAsync();
        }

       
    }
}

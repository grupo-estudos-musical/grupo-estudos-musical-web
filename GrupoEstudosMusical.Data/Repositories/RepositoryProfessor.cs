using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryProfessor : RepositoryGeneric<Professor>, IRepositoryProfessor
    {
        public async Task<Professor> ObterPorEmail(string email)
        {
            var professor = await DbSet.Where(p => p.Email == email).FirstOrDefaultAsync();
            if (professor != null)
                Context.Entry(professor).State = EntityState.Detached;
            return professor;
        }

        public async Task<List<Professor>> ObterTodosPorUsuario(int idUsuario) =>
            await DbSet.Where(p => p.UsuarioId == idUsuario).ToListAsync();
    }
}

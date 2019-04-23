using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryChamada : RepositoryGeneric<Chamada>, IRepositoryChamada
    {
        public async Task<List<Chamada>> ObterChamadasPorTurmaAsync(int idTurma)
        {
            return await DbSet
                .Include(c => c.Frequencias)
                .Include(c => c.Turma)
                .Where(c => c.IdTurma == idTurma)
                .ToListAsync();
        }

        public override async Task<Chamada> ObterPorIdAsync(int id)
        {
            return await DbSet
                .Include(c => c.Frequencias)
                .ThenInclude(f => f.Aluno)
                .Include(c => c.Turma)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}

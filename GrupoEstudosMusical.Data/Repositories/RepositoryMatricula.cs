using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryMatricula : RepositoryGeneric<Matricula>, IRepositoryMatricula
    {
        public async Task<IList<Matricula>> ObterMatriculasPorAluno(int idAluno)
        {
            return await DbSet
                .Include(m => m.Aluno)
                .Include(m => m.Turma)
                .ThenInclude(t => t.Modulo)
                .Where(m => m.AlunoId == idAluno)
                .ToListAsync();
        }
    }
}

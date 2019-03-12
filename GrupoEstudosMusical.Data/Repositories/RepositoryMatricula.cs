using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryMatricula : RepositoryGeneric<Matricula>, IRepositoryMatricula
    {
        public async Task<IList<Matricula>> ObterMatriculasPorAluno(int idAluno) =>
            await ObterMatriculas(m => m.AlunoId == idAluno);

        public override async Task<Matricula> ObterPorIdAsync(int id)
        {
            var matriculas = await ObterMatriculas(m => m.Id == id);
            return matriculas.FirstOrDefault();
        }

        public List<Matricula> ObterTurmasDoAluno(int idAluno) =>
            Context.Matriculas.Include(m => m.Turma).Where(m => m.AlunoId == idAluno).ToList();

        private async Task<IList<Matricula>> ObterMatriculas(Expression<Func<Matricula, bool>> filter)
        {
            return await DbSet
                            .Include(m => m.Aluno)
                            .Include(m => m.Turma)
                            .ThenInclude(t => t.Modulo)
                            .Include(m => m.Turma.Professor)
                            .Where(filter)
                            .ToListAsync();
        }

    }
}

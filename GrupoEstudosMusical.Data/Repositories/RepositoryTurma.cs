using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryTurma : RepositoryGeneric<Turma>, IRepositoryTurma
    {
        public override async Task<IList<Turma>> ObterTodosAsync() =>
            await DbSet.Include(t => t.Professor)
                .Include(t => t.Modulo)
                .Include(t => t.Matriculas)
                .ToListAsync();

        public override async Task<Turma> ObterPorIdAsync(int id) =>
            await DbSet.Include(t => t.Professor)
                .Include(t => t.Modulo)
                .Include(t => t.Matriculas)
                .ThenInclude(t => t.Aluno)
                .Include(t => t.Chamadas)
                .ThenInclude(c => c.Frequencias)
                .FirstOrDefaultAsync( t => t.Id==id);

        public Turma VerificarExistenciaDaTurmaPorNomePeriodoSemestre(string nomeTurma, int periodo, int semestre, int Id) =>
            DbSet.Where(t => t.Nome == nomeTurma & t.Periodo==periodo & t.Semestre==semestre & t.Id !=Id ).FirstOrDefault();

        public IList<Turma> ObterTurmasAtivasPorModulo(int moduloId) => 
            DbSet
                .Include(t => t.Matriculas)
                .Where(t => t.Status == "Ativo" && t.ModuloID == moduloId)
                .ToList();

        public IList<Turma> ObterTurmasAtivas() => 
            DbSet.Where(t => t.Status == "Ativo").ToList();

        public async Task<IList<Turma>> ObterTurmasPorAluno(int idAluno)
        {
            return await DbSet.
                Include(t => t.Matriculas)
                .Where(t => t.Matriculas.Where(m => m.AlunoId == idAluno).FirstOrDefault() != null)
                .Include(t => t.Professor)
                .Include(t => t.Modulo)
                .ToListAsync();
        }

        public List<Turma> ObterTurmasDoProfessor( int professorID)
        {
            return Context.Turmas.Where(t => t.ProfessorID == professorID)
                .Include(t => t.Modulo)
                .Include(t => t.Matriculas)
                .ThenInclude(m => m.Aluno)
                .ToList();
        }
    }
}

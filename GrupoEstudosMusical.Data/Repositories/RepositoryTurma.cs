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
            await DbSet.Include(t => t.Professor).Include(t => t.Modulo).ToListAsync();

        public override async Task<Turma> ObterPorIdAsync(int id) =>
            await DbSet.Include(t => t.Professor).Include(t => t.Modulo).FirstOrDefaultAsync( t => t.Id==id);

        public Turma VerificarExistenciaDaTurmaPorNomePeriodoSemestre(string nomeTurma, int periodo, int semestre, int Id) =>
            DbSet.Where(t => t.Nome == nomeTurma & t.Periodo==periodo & t.Semestre==semestre & t.Id !=Id ).FirstOrDefault();
            
       
    }
}

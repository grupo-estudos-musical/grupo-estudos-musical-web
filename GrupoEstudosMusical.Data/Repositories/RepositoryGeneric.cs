using GrupoEstudosMusical.Data.Context;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryGeneric<TEntity> : IRepositoryGeneric<TEntity> where TEntity : BaseEntity
    {
        protected GemContext Context;
        protected DbSet<TEntity> DbSet;

        public RepositoryGeneric()
        {
            Context = new GemContext();
            DbSet = Context.Set<TEntity>();
        }

        public async Task AlterarAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.Entry(entity).Property("DataCadastro").IsModified = false;
            await Context.SaveChangesAsync();
        }

        public async Task DeletarAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task InserirAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> ObterPorIdAsync(int id) => await DbSet.FindAsync(id);

        public virtual async Task<IList<TEntity>> ObterTodosAsync() => await DbSet.ToListAsync();
        

        public async Task<int> ObterUltimoIdAsync()
        {
            var registro = await DbSet.OrderByDescending(e => e.Id).FirstOrDefaultAsync();
            return registro.Id;
        }
    }
}

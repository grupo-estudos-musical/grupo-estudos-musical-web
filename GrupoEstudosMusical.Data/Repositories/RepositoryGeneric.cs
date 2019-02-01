using GrupoEstudosMusical.Data.Context;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryGeneric<TEntity> : IDisposable, IRepositoryGeneric<TEntity> where TEntity : class
    {
        protected GemContext Context;
        protected DbSet<TEntity> DbSet;

        public RepositoryGeneric()
        {
            Context = new GemContext();
            DbSet = Context.Set<TEntity>();
        }

        public async void Alterar(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async void Deletar(TEntity entity)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async void Inserir(TEntity entity)
        {
            DbSet.Add(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<TEntity> ObterPorIdAsync(int id) => await DbSet.FindAsync(id);

        public async Task<IList<TEntity>> ObterTodosAsync() => await DbSet.ToListAsync();

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}

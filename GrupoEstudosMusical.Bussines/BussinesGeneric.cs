using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesGeneric<TEntity> : IBussinesGeneric<TEntity> where TEntity : class
    {
        private readonly IRepositoryGeneric<TEntity> _repository;

        public BussinesGeneric(IRepositoryGeneric<TEntity> repository)
        {
            _repository = repository;
        }
        
        

        public virtual async Task AlterarAsync(TEntity entity) => await _repository.AlterarAsync(entity);

        public virtual async Task DeletarAsync(TEntity entity) => await _repository.DeletarAsync(entity);

        public virtual async Task InserirAsync(TEntity entity) => await _repository.InserirAsync(entity);

        public virtual async Task<TEntity> ObterPorIdAsync(int id) => await _repository.ObterPorIdAsync(id);

        public async Task<IList<TEntity>> ObterTodosAsync() => await _repository.ObterTodosAsync();

        public async Task<int> ObterUltimoIdAsync() => await _repository.ObterUltimoIdAsync();
    }
}

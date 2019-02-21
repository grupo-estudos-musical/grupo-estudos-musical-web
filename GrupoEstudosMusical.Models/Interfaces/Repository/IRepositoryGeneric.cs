using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryGeneric<TEntity> where TEntity : class
    {
        Task InserirAsync(TEntity entity);
        Task AlterarAsync(TEntity entity);
        Task DeletarAsync(TEntity entity);
        Task<TEntity> ObterPorIdAsync(int id);
        Task<IList<TEntity>> ObterTodosAsync();
        Task<int> ObterUltimoIdAsync();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesGeneric<TEntity> where TEntity : class
    {
        Task InserirAsync(TEntity entity);
        Task AlterarAsync(TEntity entity);
        Task DeletarAsync(TEntity entity);
        Task<TEntity> ObterPorIdAsync(int id);
        Task<IList<TEntity>> ObterTodosAsync();
        Task<int> ObterUltimoIdAsync();
    }
}

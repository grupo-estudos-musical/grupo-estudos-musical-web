using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesGeneric<TEntity> where TEntity : class
    {
        void Inserir(TEntity entity);
        void Alterar(TEntity entity);
        void Deletar(TEntity entity);
        Task<TEntity> ObterPorIdAsync(int id);
        Task<IList<TEntity>> ObterTodosAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesInventario : IBussinesGeneric<Inventario>
    {
        List<Inventario> ObterTodosItensDoInventario();
        Task ModificarInventario(Guid idInventario, int quantidadeMinima, int quantidadeDisponivel);
    }
}

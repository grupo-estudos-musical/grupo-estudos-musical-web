using System;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryInventario : IRepositoryGeneric<Inventario>
    {
        List<Inventario> ObterTodosItensDoInventario();
            Inventario ObterPorIdGuid(Guid idInventario);
    }
}

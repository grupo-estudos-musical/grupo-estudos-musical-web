using System;
using System.Collections.Generic;
using System.Linq;
using GrupoEstudosMusical.Models;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryInventario : RepositoryGeneric<Inventario>, IRepositoryInventario
    {
        public List<Inventario> ObterTodosItensDoInventario() =>
            Context.Inventarios.Include(i => i.Instrumento).ToList();
        public Inventario ObterPorIdGuid(Guid idInventario) =>
            Context.Inventarios.FirstOrDefault(i => i.InventarioID == idInventario);
    }
}

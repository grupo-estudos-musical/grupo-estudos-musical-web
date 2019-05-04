
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Linq;
using static GrupoEstudosMusical.Models.Entities.Instrumento;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryFabricante : RepositoryGeneric<Fabricante>, IRepositoryFabricante
    {
        public Fabricante ObterPorIdGuid(Guid Id) =>
            Context.Fabricantes.FirstOrDefault(f => f.IdFabricante == Id);
    }
}

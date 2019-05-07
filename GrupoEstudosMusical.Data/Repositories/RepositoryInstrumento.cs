using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryInstrumento : RepositoryGeneric<Instrumento>, IRepositoryInstrumento
    {
        public Instrumento ObterPorIdGuid(Guid Id) =>
            Context.Instrumentos.FirstOrDefault(i => i.IntrumentoID == Id);

        public Instrumento ObterPorNome(string Nome)
        {
            return Context.Instrumentos.FirstOrDefault(i => i.Nome == Nome);
        }

        public async override Task<IList<Instrumento>> ObterTodosAsync()
        {
            return await Context.Instrumentos.Include(i => i.Inventario).ToListAsync();
        }

    }
}

using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Linq;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryInstrumento : RepositoryGeneric<Instrumento>, IRepositoryInstrumento
    {
        public Instrumento ObterPorIdGuid(Guid Id) =>
            Context.Instrumentos.FirstOrDefault(i => i.IntrumentoID == Id);
        
    }
}

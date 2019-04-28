using GrupoEstudosMusical.Models.Entities;
using System;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryInstrumento : IRepositoryGeneric<Instrumento>
    {
        Instrumento ObterPorIdGuid(Guid Id);
    }
}

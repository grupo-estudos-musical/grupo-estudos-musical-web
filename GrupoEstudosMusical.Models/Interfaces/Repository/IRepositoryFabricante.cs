using System;
using static GrupoEstudosMusical.Models.Entities.Instrumento;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryFabricante : IRepositoryGeneric<Fabricante>
    {
        Fabricante ObterPorIdGuid(Guid Id);
    }
}

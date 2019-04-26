
using System;
using static GrupoEstudosMusical.Models.Entities.Instrumento;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesFabricante : IBussinesGeneric<Fabricante>
    {
        Fabricante ObterPorIdGuid(Guid Id);
    }
}

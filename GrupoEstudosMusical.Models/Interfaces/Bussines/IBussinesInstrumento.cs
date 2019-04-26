using GrupoEstudosMusical.Models.Entities;
using System;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesInstrumento: IBussinesGeneric<Instrumento>
    {
        Instrumento ObterPorIdGuid(Guid Id);
        
    }
}

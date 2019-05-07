using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesInstrumento: IBussinesGeneric<Instrumento>
    {
        Instrumento ObterPorIdGuid(Guid Id);
        Task<List<Instrumento>> ObterTodosDisponivelParaEmprestimo();
        
    }
}

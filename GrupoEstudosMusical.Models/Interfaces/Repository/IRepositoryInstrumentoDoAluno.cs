using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryInstrumentoDoAluno : IRepositoryGeneric<InstrumentoDoAluno>
    {
        InstrumentoDoAluno ObterPorIdGuid(Guid Id);
        List<InstrumentoDoAluno> ObterInstrumentosDoAluno(int alunoId);
        InstrumentoDoAluno ObterPorAlunoEInventarioGuid(int alunoID, Guid inventarioID);
    }
}

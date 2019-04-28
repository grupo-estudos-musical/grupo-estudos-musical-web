using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesInstrumentoDoAluno : IBussinesGeneric<InstrumentoDoAluno>
    {
        InstrumentoDoAluno ObterPorIdGuid(Guid Id);
        List<InstrumentoDoAluno> ObterInstrumentosDoAluno(int alunoId);
    }
}

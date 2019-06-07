using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Entities.Relatorios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesInstrumentoDoAluno : IBussinesGeneric<InstrumentoDoAluno>
    {
        InstrumentoDoAluno ObterPorIdGuid(Guid Id);
        List<InstrumentoDoAluno> ObterInstrumentosDoAluno(int alunoId);
        InstrumentoDoAluno ObterPorAlunoEInventarioGuid(int alunoID, Guid inventarioID);
        Task RealizarDevolucao(Guid instrumentoDoAlunoID);
        Task<DetalhesDoEmprestimo> ObterDadosDeEmprestimos(int alunoid);
    }
}

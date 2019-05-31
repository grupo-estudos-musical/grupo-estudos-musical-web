using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Entities.Relatorios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesOcorrencia:IBussinesGeneric<Ocorrencia>
    {
        List<Ocorrencia> ObterOcorrenciasPorAluno(int AlunoId);
        Task<OcorrenciasParaRelatorio> ObterOcorrenciasParaRelatorio(int alunoID);
        List<OcorrenciasParaRelatorio> ObterOcorrenciasPorTurma(DateTime dataInicial, DateTime dataFinal, int turmaID);

    }
}

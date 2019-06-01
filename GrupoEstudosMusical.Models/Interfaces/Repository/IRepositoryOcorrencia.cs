
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Entities.Relatorios;
using System;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryOcorrencia:IRepositoryGeneric<Ocorrencia>
    {
        List<Ocorrencia> ObterOcorrenciasPorAluno(int AlunoId);
        //List<OcorrenciasParaRelatorio> ObterOcorrenciasParaRelatorio(int AlunoId);
        List<Ocorrencia> ObterOcorrenciasPorTurma(DateTime dataInicial, DateTime dataFinal, int turmaID);
    }
}

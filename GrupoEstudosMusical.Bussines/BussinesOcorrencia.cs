using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrupoEstudosMusical.Bussines.Helpers;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Entities.Relatorios;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesOcorrencia : BussinesGeneric<Ocorrencia>, IBussinesOcorrencia
    {
        private readonly IRepositoryOcorrencia _repositoryOcorrencia;
        private readonly IRepositoryAluno _repositoryAluno;
        public BussinesOcorrencia(IRepositoryOcorrencia repository, IRepositoryAluno repositoryAluno) : base(repository)
        {
            _repositoryOcorrencia = repository;
            _repositoryAluno = repositoryAluno;
        }

        public List<Ocorrencia> ObterOcorrenciasPorAluno(int AlunoId) => 
            _repositoryOcorrencia.ObterOcorrenciasPorAluno(AlunoId);

        public async Task<OcorrenciasParaRelatorio> ObterOcorrenciasParaRelatorio(int alunoID)
        {
            var aluno = await _repositoryAluno.ObterPorIdAsync(alunoID);
            var listaDeOcorrenciasParaRelatorio = new OcorrenciasParaRelatorio()
            {
                Aluno = aluno != null ? aluno : null
            };
            
            foreach (var  ocorrencia in ObterOcorrenciasPorAluno(alunoID))
            {
                listaDeOcorrenciasParaRelatorio.Ocorrencias.Add(ocorrencia);
            }
            return listaDeOcorrenciasParaRelatorio;
        }

        public List<OcorrenciasParaRelatorio> ObterOcorrenciasPorTurma(DateTime dataInicial, DateTime dataFinal, int turmaID)
        {
            ValidacaoHelper.ValidarSeDataInicialEhMaiorQueAFinal(dataInicial, dataFinal);
            var ocorrenciasTurma = _repositoryOcorrencia.ObterOcorrenciasPorTurma(dataInicial, dataFinal, turmaID);

            var listaDeOcorrenciasParaRelatorio = new List<OcorrenciasParaRelatorio>();

            var ocorrenciasPorAluno = ocorrenciasTurma.GroupBy(o => o.AlunoID).ToList();

            foreach(var ocorrenciaAluno in ocorrenciasPorAluno)
            {
                var ocorrenciasParaRelatorio = new OcorrenciasParaRelatorio() { Aluno = ocorrenciaAluno.FirstOrDefault().Aluno };
                foreach (var ocorrencia in ocorrenciaAluno.ToList())
                {
                    ocorrenciasParaRelatorio.Ocorrencias.Add(ocorrencia);
                }
                listaDeOcorrenciasParaRelatorio.Add(ocorrenciasParaRelatorio);
            }
            
            return listaDeOcorrenciasParaRelatorio;
        }
    }
}

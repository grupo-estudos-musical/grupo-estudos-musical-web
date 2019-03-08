using System;
using System.Collections.Generic;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesOcorrencia : BussinesGeneric<Ocorrencia>, IBussinesOcorrencia
    {
        private readonly IRepositoryOcorrencia _repositoryOcorrencia;
        public BussinesOcorrencia(IRepositoryOcorrencia repository) : base(repository)
        {
            _repositoryOcorrencia = repository;
        }

        public List<Ocorrencia> ObterOcorrenciasPorAluno(int AlunoId) => 
            _repositoryOcorrencia.ObterOcorrenciasPorAluno(AlunoId);
    }
}

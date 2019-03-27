using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesAvaliacaoTurma : BussinesGeneric<AvaliacaoTurma>, IBussinesAvaliacaoTurma
    {
        readonly IRepositoryAvaliacaoTurma _repositoryAvaliacaoTurma;
        public BussinesAvaliacaoTurma(IRepositoryAvaliacaoTurma repository) : base(repository)
        {
            _repositoryAvaliacaoTurma = repository;
        }

        public List<AvaliacaoTurma> ObterPelaTurma(int turma) =>
            _repositoryAvaliacaoTurma.ObterPelaTurma(turma);


        public AvaliacaoTurma ObterPorIds(int turma, int avaliacao) =>
            _repositoryAvaliacaoTurma.ObterPorIds(turma, avaliacao);

        public override Task InserirAsync(AvaliacaoTurma entity)
        {
            VerificarExistenciaDeAvaliacaoParaTurma(entity);
            return base.InserirAsync(entity);
        }

        public override Task AlterarAsync(AvaliacaoTurma entity)
        {
            return base.AlterarAsync(entity);
        }

        public void VerificarSeDataAtualEhMaiorQueAsDemais(DateTime dataPrevista)
        {
            if (DateTime.Now > dataPrevista)
                throw new CrudAvaliacaoException("A data atual não pode ser maior que a data prevista!");
        }

        public void VerificarExistenciaDeAvaliacaoParaTurma(AvaliacaoTurma entity)
        {
            var verificaSeJaExisteAvaliacaoCriadaParaTurma = ObterPorIds(entity.TurmaID, entity.AvaliacaoID) != null ? true : false;

            if (verificaSeJaExisteAvaliacaoCriadaParaTurma)
            {
                throw new CrudAvaliacaoException("Está avaliação já foi adicionada para esta turma");
            }
        }
    }
}

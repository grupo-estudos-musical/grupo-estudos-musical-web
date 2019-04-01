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
            VerificaPossibilidadeDeCriacaoDaAvaliacao(entity);
            return base.InserirAsync(entity);
        }

        
        public override Task AlterarAsync(AvaliacaoTurma entity)
        {
            return base.AlterarAsync(entity);
        }


        public void VerificaPossibilidadeDeCriacaoDaAvaliacao(AvaliacaoTurma entity)
        {
            var verificaSeJaExisteAvaliacaoCriadaParaTurma = ObterPorIds(entity.TurmaID, entity.AvaliacaoID) != null ? true : false;

            if (verificaSeJaExisteAvaliacaoCriadaParaTurma)
            {
                throw new CrudAvaliacaoException("Está avaliação já foi adicionada para esta turma");
            }
            if (entity.DataPrevista == Convert.ToDateTime(null))
                throw new CrudAvaliacaoException("Data prevista não pode ser nula!");
        }
    }
}

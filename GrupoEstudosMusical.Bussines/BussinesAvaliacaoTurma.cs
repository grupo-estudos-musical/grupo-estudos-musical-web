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
        readonly IRepositoryTurma _repositoryTurma;
        readonly IRepositoryPalhetaDeNotas _repositoryPalhetaDeNotas;

        public BussinesAvaliacaoTurma(IRepositoryAvaliacaoTurma repository, IRepositoryTurma repositoryTurma, IRepositoryPalhetaDeNotas repositoryPalhetaDeNotas) : base(repository)
        {
            _repositoryAvaliacaoTurma = repository;
            _repositoryTurma = repositoryTurma;
            _repositoryPalhetaDeNotas = repositoryPalhetaDeNotas;
        }

        public List<AvaliacaoTurma> ObterPorTurma(int turma) =>
            _repositoryAvaliacaoTurma.ObterPorTurma(turma);

        void VerificarSeExistePalhetaDeNotaGerada(Guid avaliacaoID, int turma)
        {
            var existePalhetaDeNota = _repositoryPalhetaDeNotas.ObterPalhetasPorAvaliacaoEhTurma(avaliacaoID, turma);
            if (existePalhetaDeNota.Count > 0)
                throw new CrudAvaliacaoException("Existe palheta de notas utilizando está avaliação! \n Para remoção da avaliação apague a palheta de Notas.");
        }

        public override Task DeletarAsync(AvaliacaoTurma entity)
        {
            VerificarSeExistePalhetaDeNotaGerada(entity.IdAvaliacaoTurma, entity.TurmaID);
            return base.DeletarAsync(entity);
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

        public AvaliacaoTurma ObterPorId(Guid AvaliacaoTurmaID) =>
            _repositoryAvaliacaoTurma.ObterPorId(AvaliacaoTurmaID);

        public async Task AdicionarAvaliacaoAosAlunosDaTurma(List<Matricula> matriculasDosAlunos, Guid avaliacaoId)
        {
            foreach(var matricula in matriculasDosAlunos)
            {
               await _repositoryPalhetaDeNotas.InserirAsync(new PalhetaDeNota() { AvaliacaoID = avaliacaoId, MatriculaID = matricula.Id });
            }
            
        }
    }
    
}

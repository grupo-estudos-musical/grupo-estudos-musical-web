using System;
using System.Threading.Tasks;
using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Bussines.Helpers;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;


namespace GrupoEstudosMusical.Bussines
{
    public class BussinesAvaliacao : BussinesGeneric<Avaliacao>, IBussinesAvaliacao
    {
        readonly IRepositoryAvaliacao _repositoryAvaliacao;
        public BussinesAvaliacao(IRepositoryAvaliacao repository) : base(repository)
        {
            _repositoryAvaliacao = repository;
        }

        public override Task InserirAsync(Avaliacao entity)
        {
            ValidarAvaliacao(entity);
            VerificaExistenciaDeAvaliacao(entity.Nome, entity.Id);
            return base.InserirAsync(entity);
        }

        public override Task AlterarAsync(Avaliacao entity)
        {
            ValidarAvaliacao(entity);
            VerificaExistenciaDeAvaliacao(entity.Nome, entity.Id);
            return base.AlterarAsync(entity);
        }

        void ValidarAvaliacao(Avaliacao avaliacao)
        {
            if (avaliacao == null){
                throw new Exception("Avaliação não pode ser nula");
            }
            ValidacaoHelper.ValidaString(avaliacao.Nome);
            ValidarNumeros(avaliacao.NotaMaxima);
            ValidarNumeros(avaliacao.Peso);
        }

        public void VerificaExistenciaDeAvaliacao(string nomeAvaliacao, int Id)
        {
            var avaliacao = _repositoryAvaliacao.VerificaExistenciaDeAvaliacao(nomeAvaliacao, Id);
            if (avaliacao != null) throw new CrudAvaliacaoException($"Já existe uma avaliação criada com o nome {nomeAvaliacao} !");
        }

        void ValidarNumeros(double valor)
        {
            if (valor <=  0)
                throw new CrudAvaliacaoException($"O campo  tem que ser superior a 0");
        }
    }
}

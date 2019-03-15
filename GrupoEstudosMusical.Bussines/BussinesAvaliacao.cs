using System;
using System.Threading.Tasks;
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
            return base.InserirAsync(entity);
        }

        void ValidarAvaliacao(Avaliacao avaliacao)
        {
            if (avaliacao == null){
                throw new Exception("Avaliação não pode ser nula");
            }
            ValidacaoHelper.ValidaString(avaliacao.Nome);
        }

        void Validar(double avaliacao)
        {
            if (avaliacao <= 0)
                throw new Exception("A nota máxima tem que ser superior a 0");
        }
    }
}

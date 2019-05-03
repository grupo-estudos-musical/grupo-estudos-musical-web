

using System;
using System.Threading.Tasks;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesInstrumento : BussinesGeneric<Instrumento>, IBussinesInstrumento
    {
        readonly IRepositoryInstrumento _repositoryInstrumento;
        public BussinesInstrumento(IRepositoryInstrumento repository) : base(repository)
        {
            _repositoryInstrumento = repository;
        }
        public override Task InserirAsync(Instrumento entity)
        {
            ValidarCrudInstrumento(entity);
            return base.InserirAsync(entity);
        }
        
        void ValidarCrudInstrumento(Instrumento entity)
        {
            if (_repositoryInstrumento.ObterPorNome(entity.Nome) != null)
                throw new Exception($"Já existe um instrumento com a descrição {entity.Nome}");
        }

        public Instrumento ObterPorIdGuid(Guid Id) =>
            _repositoryInstrumento.ObterPorIdGuid(Id);
    }
}

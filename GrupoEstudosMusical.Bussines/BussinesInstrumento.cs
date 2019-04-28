

using System;
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

        public Instrumento ObterPorIdGuid(Guid Id) =>
            _repositoryInstrumento.ObterPorIdGuid(Id);
    }
}

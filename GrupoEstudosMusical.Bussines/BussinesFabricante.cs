using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GrupoEstudosMusical.Models.Entities.Instrumento;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesFabricante : BussinesGeneric<Fabricante>, IBussinesFabricante
    {
        readonly IRepositoryFabricante _repositoryFabricante;
        public BussinesFabricante(IRepositoryFabricante repository) : base(repository)
        {
            _repositoryFabricante = repository;
        }

        void VerificarExistenciaDoFabricante(Fabricante entity)
        {
            var fabricante = _repositoryFabricante.ObterPorNome(entity.Nome);
            if (fabricante != null && fabricante.Id != entity.Id)
                throw new Exception("Fabricante já cadastrado! ");
        }
        public override Task InserirAsync(Fabricante entity)
        {
            VerificarExistenciaDoFabricante(entity);
            return base.InserirAsync(entity);
        }
        public override Task AlterarAsync(Fabricante entity)
        {
            VerificarExistenciaDoFabricante(entity);
            return base.AlterarAsync(entity);
        }


        public Fabricante ObterPorIdGuid(Guid Id) =>
            _repositoryFabricante.ObterPorIdGuid(Id);
    }
}

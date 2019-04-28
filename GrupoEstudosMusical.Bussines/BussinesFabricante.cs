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

        public Fabricante ObterPorIdGuid(Guid Id) =>
            _repositoryFabricante.ObterPorIdGuid(Id);
    }
}

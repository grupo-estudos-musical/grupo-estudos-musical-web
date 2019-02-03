using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesModulo : BussinesGeneric<Modulo>, IBussinesModulo
    {
        private readonly IRepositoryModulo _repositoryModulo;
        public BussinesModulo(IRepositoryModulo repositoryModulo) : base(repositoryModulo)
        {
            _repositoryModulo = repositoryModulo;
        }
    }
}

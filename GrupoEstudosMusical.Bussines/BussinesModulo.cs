using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;

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

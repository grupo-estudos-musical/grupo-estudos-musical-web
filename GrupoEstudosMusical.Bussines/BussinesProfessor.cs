using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesProfessor : BussinesGeneric<Professor>, IBussinesProfessor
    {
        private readonly IRepositoryProfessor _repositoryProfessor;

        public BussinesProfessor(IRepositoryProfessor repositoryProfessor) : base(repositoryProfessor)
        {
            _repositoryProfessor = repositoryProfessor;
        }
    }
}

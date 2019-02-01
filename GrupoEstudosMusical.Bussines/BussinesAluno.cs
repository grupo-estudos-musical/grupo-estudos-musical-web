using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesAluno : BussinesGeneric<Aluno>, IBussinesAluno
    {
        private readonly IRepositoryAluno _repositoryAluno;

        public BussinesAluno(IRepositoryAluno repositoryAluno) : base(repositoryAluno)
        {
            _repositoryAluno = repositoryAluno;
        }

    }
}

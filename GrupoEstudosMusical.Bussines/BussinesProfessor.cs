using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesProfessor : BussinesGeneric<Professor>, IBussinesProfessor
    {
        private readonly IRepositoryProfessor _repositoryProfessor;

        public BussinesProfessor(IRepositoryProfessor repositoryProfessor) : base(repositoryProfessor)
        {
            _repositoryProfessor = repositoryProfessor;
        }

        public override Task AlterarAsync(Professor entity)
        {
            ValidarContato(entity);
            return base.AlterarAsync(entity);
        }

        public override Task InserirAsync(Professor entity)
        {
            ValidarContato(entity);
            return base.InserirAsync(entity);
        }

        private void ValidarContato(Professor professor)
        {
            if (professor.Telefone == null && professor.Celular == null)
                throw new ArgumentException("Professor deve ter ao menos um Telefone para contato.");
        }
    }
}

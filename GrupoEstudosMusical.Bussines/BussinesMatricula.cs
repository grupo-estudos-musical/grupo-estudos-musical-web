using System;
using System.Threading.Tasks;
using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesMatricula : BussinesGeneric<Matricula>, IBussinesMatricula
    {
        private readonly IRepositoryMatricula _repositoryMatricula;
        private readonly IRepositoryTurma _repositoryTurma;

        public BussinesMatricula(IRepositoryMatricula repositoryMatricula, 
            IRepositoryTurma repositoryTurma) : base(repositoryMatricula)
        {
            _repositoryMatricula = repositoryMatricula;
            _repositoryTurma = repositoryTurma;
        }

        public async override Task InserirAsync(Matricula entity)
        {
            var turma = await _repositoryTurma.ObterPorIdAsync(entity.Id);
            if (!turma.VerificarQuantidadeDeAlunosMatriculados())
                throw new TurmaMatriculaExeception("Não existe vagas para esta turma.");

            if (entity.VerificarMatriculaPendente())
                throw new ArgumentException("Matrícula ficará pendente.");
            await base.InserirAsync(entity);
        }
    }
}

using System.Collections.Generic;
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
        private readonly IRepositoryAluno _repositoryAluno;

        public BussinesMatricula(IRepositoryMatricula repositoryMatricula, 
            IRepositoryTurma repositoryTurma, IRepositoryAluno repositoryAluno) : base(repositoryMatricula)
        {
            _repositoryMatricula = repositoryMatricula;
            _repositoryTurma = repositoryTurma;
            _repositoryAluno = repositoryAluno;
        }

        public async override Task InserirAsync(Matricula entity)
        {
            var turma = await _repositoryTurma.ObterPorIdAsync(entity.TurmaId);
            if (!turma.VerificarQuantidadeDeAlunosMatriculados())
                throw new TurmaMatriculaExeception("Não existe vagas para esta turma.");

            entity.VerificarMatriculaPendente();
            entity.Aluno = null;
            await base.InserirAsync(entity);
        }

        public override Task AlterarAsync(Matricula entity)
        {
            entity.VerificarMatriculaPendente();
            return base.AlterarAsync(entity);
        }

        public Task<IList<Matricula>> ObterMatriculasPorAluno(int idAluno) =>
            _repositoryMatricula.ObterMatriculasPorAluno(idAluno);
    }
}

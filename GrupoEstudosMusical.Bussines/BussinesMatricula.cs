using System.Collections.Generic;
using System.Linq;
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
        private readonly IRepositoryPalhetaDeNotas _repositoryPalhetaDeNotas;

        public BussinesMatricula(IRepositoryMatricula repositoryMatricula,
            IRepositoryTurma repositoryTurma, IRepositoryAluno repositoryAluno, IRepositoryPalhetaDeNotas repositoryPalhetaDeNotas) : base(repositoryMatricula)
        {
            _repositoryMatricula = repositoryMatricula;
            _repositoryTurma = repositoryTurma;
            _repositoryAluno = repositoryAluno;
            _repositoryPalhetaDeNotas = repositoryPalhetaDeNotas;
        }

        public async override Task InserirAsync(Matricula entity)
        {
            var turma = await _repositoryTurma.ObterPorIdAsync(entity.TurmaId);
            if (!turma.VerificarQuantidadeDeAlunosMatriculados())
                throw new TurmaMatriculaExeception("Não existe vagas para esta turma.");

            var matriculas = await _repositoryMatricula.ObterMatriculasPorAluno(entity.AlunoId);
            if (matriculas.Any(m => m.TurmaId == entity.TurmaId))
                throw new TurmaMatriculaExeception("Aluno já está matrícula nesta turma.");

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

        public async Task<int> IncluirMatricula(Matricula matricula)
        {
            var turma = await _repositoryTurma.ObterPorIdAsync(matricula.TurmaId);
            if (!turma.VerificarQuantidadeDeAlunosMatriculados())
                throw new TurmaMatriculaExeception("Não existe vagas para esta turma.");

            var matriculas = await _repositoryMatricula.ObterMatriculasPorAluno(matricula.AlunoId);
            if (matriculas.Any(m => m.TurmaId == matricula.TurmaId))
                throw new TurmaMatriculaExeception("Aluno já está matrícula nesta turma.");

            matricula.VerificarMatriculaPendente();
            matricula.Aluno = null;
            return await _repositoryMatricula.IncluirMatricula(matricula);
        }
    }
}

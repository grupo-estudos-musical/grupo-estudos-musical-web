using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Bussines.Helpers;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesTurma : BussinesGeneric<Turma>, IBussinesTurma
    {
        private readonly IRepositoryTurma _repositoryTurma;
        private readonly IBussinesMatricula _bussinesMatricula;
        private readonly IRepositoryPalhetaDeNotas _repositoryPalhetaDeNotas;
        public BussinesTurma(IRepositoryTurma repositoryTurma, IBussinesMatricula bussinesMatricula, IRepositoryPalhetaDeNotas repositoryPalhetaDeNotas) : base(repositoryTurma)
        {
            _repositoryPalhetaDeNotas = repositoryPalhetaDeNotas;
            _repositoryTurma = repositoryTurma;
            _bussinesMatricula = bussinesMatricula;
        }

        public async override Task InserirAsync(Turma entity)
        {
            VerificaDatas(entity.DataInicio, entity.TerminoAula);
            VerificaExistenciaDaTurmaPorNomePeriodoSemestre(entity.Nome, entity.Periodo, entity.Semestre, entity.Id);
            VerificaCampoQuantidadeDeAlunos(entity);
            await base.InserirAsync(entity);
        }
        public void VerificaCampoQuantidadeDeAlunos(Turma entity)
        {
            if (entity.QuantidadeAlunos <= 0)
                throw new CrudTurmaException("A quantidade de alunos na turma não pode ser nferior a 1");
        }
        public void VerificaDatas(DateTime dataInicio, DateTime terminoAula)
        {
            if (dataInicio > terminoAula)
            {
                throw new CrudTurmaException("A data de início não pode ser superior do término das aulas!");
            }
        }
        public async override Task AlterarAsync(Turma entity)
        {
            VerificaDatas(entity.DataInicio, entity.TerminoAula);
            VerificaExistenciaDaTurmaPorNomePeriodoSemestre(entity.Nome, entity.Periodo, entity.Semestre, entity.Id);
            VerificaCampoQuantidadeDeAlunos(entity);
            await base.AlterarAsync(entity);
        }
        public void VerificaExistenciaDaTurmaPorNomePeriodoSemestre(string nomeTurma, int periodo, int semestre, int Id)
        {
            var turmaFiltrada = _repositoryTurma.VerificarExistenciaDaTurmaPorNomePeriodoSemestre(nomeTurma, periodo, semestre, Id);

            if (turmaFiltrada != null)
            {
                throw new CrudTurmaException($"Já existe uma turma com este nome, vinculada ao {semestre}º semestre do período de {periodo}");
            }
        }
        public async override Task<Turma> ObterPorIdAsync(int id)
        {
            return await _repositoryTurma.ObterPorIdAsync(id);
        }

        public async Task<List<Turma>> ObterTurmasDoAluno(int IdAluno)
        {
            var matriculas = await _bussinesMatricula.ObterMatriculasPorAluno(IdAluno);

            var turmas = new List<Turma>();

            foreach (var matricula in matriculas)
            {
                turmas.Add(new Turma() { Id = matricula.TurmaId, Nome = matricula.Turma.Nome });
            }

            return turmas;
        }

        public async Task RecalculoAcademico(int TurmaId)
        {
            var turma = await _repositoryTurma.ObterPorIdAsync(TurmaId);

            foreach (var matricula in turma.Matriculas)
            {
                matricula.Media = CalculoDeMediaHelper.CalcularMediaDoAluno(_repositoryPalhetaDeNotas.ObterPalhetasPorMatricula(matricula.Id));
                await _bussinesMatricula.AlterarAsync(matricula);
            }
        }

        public IList<Turma> ObterTurmasAtivasPorModulo(int moduloId) => _repositoryTurma.ObterTurmasAtivasPorModulo(moduloId);

        public async Task FinalizarVigencia(int turmaId)
        {
            try
            {
                var turma = await ObterPorIdAsync(turmaId);
                if (turma == null)
                    throw new Exception("Turma não encontrada");

                turma.Status = "Encerrada";
                turma.TerminoAula = DateTime.Now;
                //var matriculas = turma.Matriculas;
                //turma.Matriculas = null;
                await _repositoryTurma.AlterarAsync(turma);
                await ConcluirSituacaoAcademicaDosAlunos(turma.Matriculas);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public async Task ConcluirSituacaoAcademicaDosAlunos(List<Matricula> matriculas)
        {
            await _bussinesMatricula.ConcluirMatriculaDoAluno(matriculas);
        }

        public IList<Turma> ObterTurmasAtivas() => _repositoryTurma.ObterTurmasAtivas();

        public async Task<IList<Turma>> ObterTurmasPorAluno(int idAluno) =>
            await _repositoryTurma.ObterTurmasPorAluno(idAluno);
    }
}

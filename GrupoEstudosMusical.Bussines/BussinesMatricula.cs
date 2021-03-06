﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Bussines.StaticList;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Entities.Relatorios;
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
        private readonly IRepositoryModulo _repositoryModulo;
        public BussinesMatricula(IRepositoryMatricula repositoryMatricula,
            IRepositoryTurma repositoryTurma, IRepositoryAluno repositoryAluno, IRepositoryPalhetaDeNotas repositoryPalhetaDeNotas, IRepositoryModulo repositoryModulo) : base(repositoryMatricula)
        {
            _repositoryMatricula = repositoryMatricula;
            _repositoryTurma = repositoryTurma;
            _repositoryAluno = repositoryAluno;
            _repositoryPalhetaDeNotas = repositoryPalhetaDeNotas;
            _repositoryModulo = repositoryModulo;
        }

        public async Task<bool> VerificarRestricaoMatricula(int alunoID)
        {
            var existeRestricao = false;
            var matriculas = await ObterMatriculasPorAluno(alunoID);
            foreach (var matricula in matriculas.Where(m => m.Status == StatusDeMatriculaStaticList.Retido))
            {
                existeRestricao = true;
                foreach (var item in matriculas.Where(m => m.Status == StatusDeMatriculaStaticList.EmAndamento))
                {
                    existeRestricao = matricula.Turma.ModuloID == item.Turma.ModuloID && matricula.Turma.Nivel == item.Turma.Nivel ? false : true;
                }
            }
            return existeRestricao;
        }

        public async Task<Boletim> ObterBoletimAluno(int matriculaID)
        {

            var matriculaAluno = await _repositoryMatricula.ObterPorIdAsync(matriculaID);
            var palhetasDeNotas = _repositoryPalhetaDeNotas.ObterPalhetasPorMatricula(matriculaID);

            if (matriculaAluno == null || palhetasDeNotas.Count <= 0)
                throw new Exception("Não há dados a serem apresentados!");

            return new Boletim()
            {
                MatriculaAluno = matriculaAluno,
                PalhetasDeNotasDoAluno = palhetasDeNotas
            };

        }

        public async Task<List<Boletim>> ObterBoletimDaTurma(int turmaID)
        {
            var turma = await _repositoryTurma.ObterPorIdAsync(turmaID);
            var boletimDaTurma = new List<Boletim>();
            foreach (var matricula in turma.Matriculas)
            {
                var boletim = await ObterBoletimAluno(matricula.Id);
                boletimDaTurma.Add(boletim);
            }

            if (boletimDaTurma.Count <= 0)
                throw new Exception("Não há dados a serem apresentados");
            return boletimDaTurma;
        }

        //public async override Task InserirAsync(Matricula entity)
        //{
        //    var turma = await _repositoryTurma.ObterPorIdAsync(entity.TurmaId);
        //    if (!turma.VerificarQuantidadeDeAlunosMatriculados())
        //        throw new TurmaMatriculaExeception("Não existe vagas para esta turma.");

        //    var matriculas = await _repositoryMatricula.ObterMatriculasPorAluno(entity.AlunoId);
        //    if (matriculas.Any(m => m.TurmaId == entity.TurmaId))
        //        throw new TurmaMatriculaExeception("Aluno já está matrícula nesta turma.");

        //    entity.VerificarMatriculaPendente();
        //    entity.Aluno = null;
        //    await base.InserirAsync(entity);
        //}

        public async Task ConcluirMatriculaDoAluno(List<Matricula> matriculas)
        {
            foreach (var matricula in matriculas)
            {
                var quantFaltas = matricula.Turma.Chamadas
                    .Where(c =>
                        c.Frequencias
                        .Where(f => f.IdAluno == matricula.AlunoId && !f.Presenca)
                        .ToList().Count > 0)
                    .Count();
                matricula.Faltas = quantFaltas;
                matricula.Status = matricula.Media < 6 || matricula.Media == null ? StatusDeMatriculaStaticList.Retido : StatusDeMatriculaStaticList.Aprovado;
                await _repositoryMatricula.AlterarSituacaoAcademicaAluno(matricula);
            }
            
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
        public async Task<List<Matricula>> ObterMatriculasPorTurma(int idTurma) => await _repositoryMatricula.ObterMatriculasPorTurma(idTurma);

        public async Task<AtestadoDeMatricula> ObterAtestadoDeMatricula(int matriculaID)
        {
            var matricula = await _repositoryMatricula.ObterPorIdAsync(matriculaID);
            if (matricula == null)
                throw new Exception("Matrícula não encontrada!");
            var atestado = new AtestadoDeMatricula()
            {
                DataMatricula = DateTime.Now,
                Matricula = matricula,
                Modulo = await _repositoryModulo.ObterPorIdAsync(matricula.Turma.ModuloID)
            };
            return atestado;
        }
           


        public async Task<List<Modulo>> ObterModulosEmQueAlunoEstaRetido(int alunoID)
        {
            List<Modulo> modulos = new List<Modulo>();
            var matriculasRetidas = await ObterMatriculaRetidasDoAluno(alunoID);
            foreach(var matricula in matriculasRetidas)
            {
                modulos.Add(matricula.Turma.Modulo);
            }
            return modulos;
        }

        public async Task<List<Matricula>> ObterMatriculaRetidasDoAluno(int alunoID)
        => await _repositoryMatricula.ObterMatriculaRetidasDoAluno(alunoID);
    }
}

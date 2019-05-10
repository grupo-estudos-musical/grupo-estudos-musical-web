﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GrupoEstudosMusical.Bussines.StaticList;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryMatricula : RepositoryGeneric<Matricula>, IRepositoryMatricula
    {
        public async Task<int> IncluirMatricula(Matricula matricula)
        {
            await DbSet.AddAsync(matricula);
            await Context.SaveChangesAsync();
            return matricula.Id;
        }

        public async Task<List<Matricula>> ObterMatriculaRetidasDoAluno(int alunoID)
        {
            var matriculas = await ObterMatriculasPorAluno(alunoID);

            return matriculas.Where(m => m.Status == StatusDeMatriculaStaticList.Retido).ToList();
        }

        public async Task<IList<Matricula>> ObterMatriculasPorAluno(int idAluno) =>
            await ObterMatriculas(m => m.AlunoId == idAluno);

        public async Task<List<Matricula>> ObterMatriculasPorTurma(int idTurma)
        {
            return await DbSet
                .Include(m => m.Aluno)
                .Where(m => m.TurmaId == idTurma)
                .ToListAsync();
        }

        public override async Task<Matricula> ObterPorIdAsync(int id)
        {
            var matriculas = await ObterMatriculas(m => m.Id == id);
            return matriculas.FirstOrDefault();
        }


        private async Task<IList<Matricula>> ObterMatriculas(Expression<Func<Matricula, bool>> filter)
        {
            return await DbSet
                            .Include(m => m.Aluno)
                            .Include(m => m.Turma)
                            .ThenInclude(t => t.Modulo)
                            .Include(m => m.Turma.Professor)
                            .Where(filter)
                            .ToListAsync();
        }
    }
}

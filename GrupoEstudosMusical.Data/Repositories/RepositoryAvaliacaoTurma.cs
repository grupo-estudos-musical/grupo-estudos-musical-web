﻿using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryAvaliacaoTurma : RepositoryGeneric<AvaliacaoTurma>, IRepositoryAvaliacaoTurma
    {
        public List<AvaliacaoTurma> ObterPorTurma(int turma) =>
            DbSet.Include(t => t.Avaliacao)
            .Include(t => t.Turma)
            .Include(t => t.Avaliacao)
            .Where(a => a.TurmaID == turma).ToList();

        public AvaliacaoTurma ObterPorId(Guid AvaliacaoTurmaID) =>
            DbSet.Include(a => a.Avaliacao)
            .Include(a => a.Turma)
            .Where(a => a.IdAvaliacaoTurma == AvaliacaoTurmaID).FirstOrDefault();

        public AvaliacaoTurma ObterPorIds(int turma, int avaliacao) =>
            DbSet.Include(t => t.Avaliacao)
            .Include(t => t.Turma).Where(a => a.AvaliacaoID == avaliacao && a.TurmaID == turma).FirstOrDefault();

        public async Task<List<AvaliacaoTurma>> ObterPorAula(int idAula) =>
            await DbSet.Where(t => t.AulaId == idAula).ToListAsync();
    }
}

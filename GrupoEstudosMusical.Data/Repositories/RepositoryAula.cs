﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryAula : RepositoryGeneric<Aula>, IRepositoryAula
    {
        public override async Task<Aula> ObterPorIdAsync(int id)
        {
            return await DbSet.Include(a => a.Turma)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Aula>> ObterPorTurma(int idTurma)
        {
            var aulas = await DbSet
                .Include(a => a.Turma)
                .ThenInclude(t => t.Modulo)
                .Where(a => a.TurmaId == idTurma)
                .ToListAsync();
            aulas.ForEach(a => a.AvaliacoesTurma = ObterAvaliacoesPorAula(a.Id));
            return aulas;
        }

        private List<AvaliacaoTurma> ObterAvaliacoesPorAula(int idAula) =>
            Context.AvaliacaoTurmas.Where(a => a.AulaId.Value == idAula).ToList();
    }
}

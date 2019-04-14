using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryAvaliacaoTurma : RepositoryGeneric<AvaliacaoTurma>, IRepositoryAvaliacaoTurma
    {
        public List<AvaliacaoTurma> ObterPelaTurma(int turma) =>
            DbSet.Include(t => t.Avaliacao)
            .Include(t => t.Turma)
            .Where(a => a.TurmaID == turma).ToList();

        public AvaliacaoTurma ObterPorId(Guid AvaliacaoTurmaID) =>
            DbSet.Include(a => a.Avaliacao)
            .Include(a => a.Turma)
            .Where(a => a.IdAvaliacaoTurma == AvaliacaoTurmaID).FirstOrDefault();

        public AvaliacaoTurma ObterPorIds(int turma, int avaliacao) =>
            DbSet.Include(t => t.Avaliacao)
            .Include(t => t.Turma).Where(a => a.AvaliacaoID == avaliacao && a.TurmaID == turma).FirstOrDefault();

        
        
    }
}

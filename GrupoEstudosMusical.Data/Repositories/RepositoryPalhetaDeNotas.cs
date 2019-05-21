using System;
using System.Collections.Generic;
using System.Linq;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryPalhetaDeNotas : RepositoryGeneric<PalhetaDeNota>, IRepositoryPalhetaDeNotas
    {
        public IList<PalhetaDeNota> ObterPalhetasPorAvaliacaoEhTurma(Guid avaliacaoID, int turmaID) =>
            Context.PalhetaDeNotas.Include(m => m.Matricula)
            .ThenInclude(m => m.Aluno)
            .Where(p => p.Matricula.TurmaId == turmaID && p.AvaliacaoID == avaliacaoID).ToList();

        public PalhetaDeNota ObterPorId(Guid id) =>
            Context.PalhetaDeNotas.Include(m => m.Matricula)
            .ThenInclude(m => m.Aluno)
            .Where(p => p.IdPalheta == id).FirstOrDefault();

        public List<PalhetaDeNota> ObterPalhetasPorMatricula(int matricula) =>
            Context.PalhetaDeNotas.Include(p => p.AvaliacaoTurma).ThenInclude(p => p.Avaliacao).
            Where(p => p.MatriculaID == matricula).ToList();
        
        
    }
}

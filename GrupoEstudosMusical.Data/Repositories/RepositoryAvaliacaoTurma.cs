using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryAvaliacaoTurma : RepositoryGeneric<AvaliacaoTurma>, IRepositoryAvaliacaoTurma
    {
        public List<AvaliacaoTurma> ObterPelaTurma(int turma) =>
            DbSet.Where(a => a.TurmaID == turma).ToList();
       
        public AvaliacaoTurma ObterPorIds(int turma, int avaliacao) =>
            DbSet.Where(a => a.AvaliacaoID == avaliacao && a.TurmaID == turma).FirstOrDefault();
    }
}

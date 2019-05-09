using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesTurma : IBussinesGeneric<Turma>
    {
        Task<List<Turma>> ObterTurmasDoAluno(int IdAluno);
        IList<Turma> ObterTurmasAtivasPorModulo(int moduloId);
        Task RecalculoAcademico(int TurmaId);
        IList<Turma> ObterTurmasAtivas();
    }
}

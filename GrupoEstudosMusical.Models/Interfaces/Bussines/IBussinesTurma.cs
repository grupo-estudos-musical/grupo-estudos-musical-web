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
        Task FinalizarVigencia(int turmaId);
        IList<Turma> ObterTurmasAtivas();
        Task<IList<Turma>> ObterTurmasPorAluno(int idAluno);

        List<Turma> ObterTurmasDoProfessor(int professorID);
    }
}

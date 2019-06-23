using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryTurma: IRepositoryGeneric<Turma>
    {
        Turma VerificarExistenciaDaTurmaPorNomePeriodoSemestre(string nomeTurma, int periodo, int semestre, int id);
        IList<Turma> ObterTurmasAtivasPorModulo(int moduloId);
        IList<Turma> ObterTurmasAtivas();
        Task<IList<Turma>> ObterTurmasPorAluno(int idAluno);
        List<Turma> ObterTurmasDoProfessor(int professorID);
    }
}

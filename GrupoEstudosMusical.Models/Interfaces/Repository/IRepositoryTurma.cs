using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryTurma: IRepositoryGeneric<Turma>
    {
        Turma VerificarExistenciaDaTurmaPorNomePeriodoSemestre(string nomeTurma, int periodo, int semestre, int id);
        IList<Turma> ObterTurmasAtivasPorModulo(int moduloId);
    }
}

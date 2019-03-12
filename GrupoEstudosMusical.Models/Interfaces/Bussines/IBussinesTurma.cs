using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesTurma : IBussinesGeneric<Turma>
    {
        List<Turma> ObterTurmasDoAluno(int IdAluno);
        
    }
}

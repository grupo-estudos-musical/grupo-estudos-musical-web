using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesMatricula : IBussinesGeneric<Matricula>
    {
        Task<IList<Matricula>> ObterMatriculasPorAluno(int idAluno);
        List<Matricula> ObterTurmasDoAluno(int idAluno);
    }
}

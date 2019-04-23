using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesMatricula : IBussinesGeneric<Matricula>
    {
        Task<IList<Matricula>> ObterMatriculasPorAluno(int idAluno);
        Task<List<Matricula>> ObterMatriculasPorTurma(int idTurma);
        int IncluirMatricula(Matricula matricula);
        Task<int> IncluirMatricula(Matricula matricula);
    }
}

using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesMatricula : IBussinesGeneric<Matricula>
    {
        Task<IList<Matricula>> ObterMatriculasPorAluno(int idAluno);
        Task<List<Matricula>> ObterMatriculasPorTurma(int idTurma);
        Task<int> IncluirMatricula(Matricula matricula);
        Task ConcluirMatriculaDoAluno(List<Matricula> matriculas);
        Task<bool> VerificarRestricaoMatricula(int alunoID);
        Task<List<Matricula>> ObterMatriculaRetidasDoAluno(int alunoID);
        Task<List<Modulo>> ObterModulosEmQueAlunoEstaRetido(int alunoID);
    }
}

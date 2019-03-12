using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryMatricula : IRepositoryGeneric<Matricula>
    {
        Task<IList<Matricula>> ObterMatriculasPorAluno(int idAluno);
        List<Matricula> ObterTurmasDoAluno(int idAluno);
        
    }
}

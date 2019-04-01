
using GrupoEstudosMusical.Models.Entities;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryPalhetaDeNotas: IRepositoryGeneric<PalhetaDeNota>
    {
        double CalculaMediaAluno(int AlunoID, int MatriculaID);
    }
}

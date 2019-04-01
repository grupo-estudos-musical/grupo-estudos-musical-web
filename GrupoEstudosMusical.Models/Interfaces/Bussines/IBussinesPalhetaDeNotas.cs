using GrupoEstudosMusical.Models.Entities;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    interface IBussinesPalhetaDeNotas:IBussinesGeneric<PalhetaDeNota>
    {
        double CalculaMediaAluno(int AlunoID, int MatriculaID);
    }
}

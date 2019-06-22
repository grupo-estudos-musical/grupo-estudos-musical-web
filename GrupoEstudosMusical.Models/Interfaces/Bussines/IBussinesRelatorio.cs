
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesRelatorio
    {
        Task<bool> VerificarExistenciaDeDadosParaRelatorio(int alunoID, int tipoDeRelatorio);
    }
}

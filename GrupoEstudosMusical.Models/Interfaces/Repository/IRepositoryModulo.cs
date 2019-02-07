using GrupoEstudosMusical.Models.Entities;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryModulo:IRepositoryGeneric<Modulo>
    {
        Modulo VerificaExistenciaDoModuloPorNome(string nomeModul, int Id);
    }
}

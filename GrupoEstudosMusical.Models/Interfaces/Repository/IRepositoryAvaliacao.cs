
using GrupoEstudosMusical.Models.Entities;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryAvaliacao : IRepositoryGeneric<Avaliacao>
    {
        Avaliacao VerificaExistenciaDeAvaliacao(string nomeAvaliacao, int Id);
    }
}

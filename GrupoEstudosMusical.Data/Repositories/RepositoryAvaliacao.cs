
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryAvaliacao : RepositoryGeneric<Avaliacao>, IRepositoryAvaliacao
    {
        public Avaliacao VerificaExistenciaDeAvaliacao(string nomeAvaliacao, int Id) =>
            DbSet.Where(a => a.Nome == nomeAvaliacao && a.Id != Id).FirstOrDefault();
    }
}

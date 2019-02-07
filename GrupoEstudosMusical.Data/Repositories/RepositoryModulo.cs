using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryModulo : RepositoryGeneric<Modulo>, IRepositoryModulo
    {
        public Modulo VerificaExistenciaDoModuloPorNome(string nomeModulo) =>
            DbSet.Where(m => m.Nome == nomeModulo).FirstOrDefault();
       
    }
}

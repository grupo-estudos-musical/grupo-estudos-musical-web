using System.Linq;
using System.Threading.Tasks;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryUsuario : RepositoryGeneric<Usuario>, IRepositoryUsuario
    {
        public async Task<Usuario> AutenticarAsync(string email, string senha) =>
            await DbSet.Where(u => u.Email == email && u.Senha == senha).FirstOrDefaultAsync();

        public async Task<Usuario> ObterPorSenhaAsync(string senha) =>
            await DbSet.Where(u => u.Senha == senha).FirstOrDefaultAsync();
    }
}

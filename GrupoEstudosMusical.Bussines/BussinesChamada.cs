using System.Collections.Generic;
using System.Threading.Tasks;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesChamada : BussinesGeneric<Chamada>, IBussinesChamada
    {
        private readonly IRepositoryChamada _repositoryChamada;

        public BussinesChamada(IRepositoryChamada repositoryChamada) : base(repositoryChamada)
        {
            _repositoryChamada = repositoryChamada;
        }

        public async Task<List<Chamada>> ObterChamadasPorTurmaAsync(int idTurma) => await _repositoryChamada.ObterChamadasPorTurmaAsync(idTurma);
    }
}
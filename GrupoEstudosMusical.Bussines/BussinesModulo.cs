using System;
using System.Threading.Tasks;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesModulo : BussinesGeneric<Modulo>, IBussinesModulo
    {
        private readonly IRepositoryModulo _repositoryModulo;
        public BussinesModulo(IRepositoryModulo repositoryModulo) : base(repositoryModulo)
        {
            _repositoryModulo = repositoryModulo;
        }

        public override async Task InserirAsync(Modulo modulo)
        {
            VerificaExistenciaDoModuloPorNome(modulo.Nome, modulo.Id);
            await base.InserirAsync(modulo);
        }
        public override Task AlterarAsync(Modulo entity)
        {
            VerificaExistenciaDoModuloPorNome(entity.Nome, entity.Id);
            return base.AlterarAsync(entity);
        }

        public void VerificaExistenciaDoModuloPorNome(string nomeModulo, int Id)
        {
            var moduloFiltrado = _repositoryModulo.VerificaExistenciaDoModuloPorNome(nomeModulo,Id);
            if (moduloFiltrado != null) throw new ArgumentException("Já existe um módulo cadastrado com este nome!");
        }
            
        


    }
}

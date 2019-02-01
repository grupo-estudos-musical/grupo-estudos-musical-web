using GrupoEstudosMusical.Bussines;
using GrupoEstudosMusical.Data.Repositories;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using SimpleInjector;

namespace GrupoEstudosMusical.IoC
{
    public static class DiContainer
    {
        public static Container RegisterDependencies()
        {
            var container = new Container();

            container.Register(typeof(IRepositoryGeneric<>), typeof(RepositoryGeneric<>));
            container.Register<IRepositoryAluno, RepositoryAluno>();

            container.Register(typeof(IBussinesGeneric<>), typeof(BussinesGeneric<>));
            container.Register<IBussinesAluno, BussinesAluno>();

            return container;
        }
    }
}

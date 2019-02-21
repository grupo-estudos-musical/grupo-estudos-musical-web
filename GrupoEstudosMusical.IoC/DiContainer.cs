using GrupoEstudosMusical.Bussines;
using GrupoEstudosMusical.Data.Repositories;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using MosaicoSolutions.ViaCep;
using MosaicoSolutions.ViaCep.Net;
using MosaicoSolutions.ViaCep.Util;
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
            container.Register<IRepositoryProfessor, RepositoryProfessor>();
            container.Register<IRepositoryModulo, RepositoryModulo>();
            container.Register<IRepositoryTurma, RepositoryTurma>();
            container.Register<IRepositoryOcorrencia, RepositoryOcorrencia>();
            container.Register<IRepositoryMatricula, RepositoryMatricula>();

            container.Register(typeof(IBussinesGeneric<>), typeof(BussinesGeneric<>));
            container.Register<IBussinesAluno, BussinesAluno>();
            container.Register<IBussinesProfessor, BussinesProfessor>();
            container.Register<IBussinesModulo, BussinesModulo>();
            container.Register<IBussinesTurma, BussinesTurma>();
            container.Register<IBussinesMatricula, BussinesMatricula>();
            container.Register<IBussinesOcorrencia, BussinesOcorrencia>();

            container.Register<IViaCepService, ViaCepService>();
            container.Register<IViaCepCliente, ViaCepCliente>();
            container.Register<IEnderecoConvert, EnderecoConvert>();
            container.Register<IViaCepRequisicaoPorCepFactory, ViaCepRequisicaoPorCepFactory>();
            container.Register<IViaCepRequisicaoPorEnderecoFactory, ViaCepRequisicaoPorEnderecoFactory>();

            return container;
        }
    }
}

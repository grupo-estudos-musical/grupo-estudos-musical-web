using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;


namespace GrupoEstudosMusical.Bussines
{
    public class BussinesOcorrencia : BussinesGeneric<Ocorrencia>, IBussinesOcorrencia
    {
        private readonly IRepositoryOcorrencia _repositoryOcorrencia;
        public BussinesOcorrencia(IRepositoryOcorrencia repository) : base(repository)
        {
            _repositoryOcorrencia = repository;
        }
    }
}

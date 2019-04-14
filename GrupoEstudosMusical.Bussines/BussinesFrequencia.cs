using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System.Collections.Generic;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesFrequencia : BussinesGeneric<Frequencia>, IBussinesFrequencia
    {
        private readonly IRepositoryFrequencia _repositoryFrequencia;

        public BussinesFrequencia(IRepositoryFrequencia repositoryFrequencia) : base(repositoryFrequencia)
        {
            _repositoryFrequencia = repositoryFrequencia;
        }

        public void Inserir(List<Frequencia> frequencias)
        {
            frequencias.ForEach(async frequencia =>
            {
                await _repositoryFrequencia.InserirAsync(frequencia);
            });
        }
    }
}

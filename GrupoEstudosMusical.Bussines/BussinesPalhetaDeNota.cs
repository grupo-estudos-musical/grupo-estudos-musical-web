using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesPalhetaDeNota : BussinesGeneric<PalhetaDeNota>, IBussinesPalhetaDeNotas
    {
        readonly IRepositoryPalhetaDeNotas _repositoryPalhetaDeNotas;
        public BussinesPalhetaDeNota(IRepositoryPalhetaDeNotas repository) : base(repository)
        {
            _repositoryPalhetaDeNotas = repository;
        }

        public async Task AdicionarTodasAvaliacoesDaTurmaAoALuno(List<AvaliacaoTurma> avaliacoesTurma, int matriculaId)
        {
            foreach(var avaliacaoTurma in avaliacoesTurma)
            {
               await _repositoryPalhetaDeNotas.InserirAsync(new PalhetaDeNota() { AvaliacaoID = avaliacaoTurma.IdAvaliacaoTurma, MatriculaID = matriculaId });
            }
        }

        public IList<PalhetaDeNota> ObterPalhetasPorAvaliacaoEhTurma(Guid avaliacaoID, int turmaID) =>
            _repositoryPalhetaDeNotas.ObterPalhetasPorAvaliacaoEhTurma(avaliacaoID, turmaID);

        public PalhetaDeNota ObterPorId(Guid id) =>
            _repositoryPalhetaDeNotas.ObterPorId(id);

        public override Task AlterarAsync(PalhetaDeNota entity)
        {
            return base.AlterarAsync(entity);
        }

        
    }
}

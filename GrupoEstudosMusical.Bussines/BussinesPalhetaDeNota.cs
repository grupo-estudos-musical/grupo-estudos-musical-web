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

        public void AdicionarTodasAvaliacoesDaTurmaAoALuno(List<AvaliacaoTurma> avaliacoesTurma, int matriculaId)
        {
            foreach(var avaliacaoTurma in avaliacoesTurma)
            {
                _repositoryPalhetaDeNotas.InserirAsync(new PalhetaDeNota() { AvaliacaoID = avaliacaoTurma.IdAvaliacaoTurma, MatriculaID = matriculaId });
            }
        }
    }
}

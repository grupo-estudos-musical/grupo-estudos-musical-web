using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesAula : BussinesGeneric<Aula>, IBussinesAula
    {
        private readonly IRepositoryAula _repositoryAula;
        private readonly IRepositoryAvaliacaoTurma _repositoryAvaliacaoTurma;

        public BussinesAula(IRepositoryAula repositoryAula, IRepositoryAvaliacaoTurma repositoryAvaliacaoTurma) : 
            base(repositoryAula)
        {
            _repositoryAula = repositoryAula;
            _repositoryAvaliacaoTurma = repositoryAvaliacaoTurma;
        }

        public async Task InserirAsync(Aula aula, List<AvaliacaoTurma> avaliacoesTurma)
        {
            await _repositoryAula.InserirAsync(aula);

            foreach (AvaliacaoTurma avaliacaoTurma in avaliacoesTurma)
            {
                if (avaliacaoTurma.AulaId != null && avaliacaoTurma.AulaId != 0)
                    throw new ArgumentException("Avaliação já está relacionada com outra aula.");

                avaliacaoTurma.AulaId = aula.Id;
                await _repositoryAvaliacaoTurma.AlterarAsync(avaliacaoTurma);
            }
        }
    }
}

using System.Collections.Generic;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesAvaliacaoTurma : BussinesGeneric<AvaliacaoTurma>, IBussinesAvaliacaoTurma
    {
        readonly IRepositoryAvaliacaoTurma _repositoryAvaliacaoTurma;
        public BussinesAvaliacaoTurma(IRepositoryAvaliacaoTurma repository) : base(repository)
        {
            _repositoryAvaliacaoTurma = repository;
        }

        public List<AvaliacaoTurma> ObterPelaTurma(int turma) =>
            _repositoryAvaliacaoTurma.ObterPelaTurma(turma);


        public AvaliacaoTurma ObterPorIds(int turma, int avaliacao) =>
            _repositoryAvaliacaoTurma.ObterPorIds(turma, avaliacao);
        
    }
}

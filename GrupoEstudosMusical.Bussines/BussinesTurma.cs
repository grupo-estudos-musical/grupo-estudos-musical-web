using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesTurma : BussinesGeneric<Turma>, IBussinesTurma
    {
        private readonly IRepositoryTurma _repositoryTurma;
        public BussinesTurma(IRepositoryTurma repositoryTurma) : base(repositoryTurma)
        {
            _repositoryTurma = repositoryTurma;
        }

        public async override Task InserirAsync(Turma entity)
        {
            VerificaDatas(entity.DataInicio, entity.TerminoAula);
            await base.InserirAsync(entity);
        }
        public void VerificaDatas(DateTime dataInicio, DateTime terminoAula)
        {
            if (dataInicio > terminoAula)
            {
                throw new ArgumentException("A data de início não pode ser superior do término das aulas!");
            }
        }

        public async override Task AlterarAsync(Turma entity)
        {
            VerificaDatas(entity.DataInicio, entity.TerminoAula);
            await base.AlterarAsync(entity);
        }

        

       
    }
}

using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
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
            VerificaExistenciaDaTurmaPorNomePeriodoSemestre(entity.Nome, entity.Periodo, entity.Semestre);
            await base.InserirAsync(entity);
        }
        public void VerificaDatas(DateTime dataInicio, DateTime terminoAula)
        {
            if (dataInicio > terminoAula)
            {
                throw new CrudTurmaException("A data de início não pode ser superior do término das aulas!");
            }
        }
        public async override Task AlterarAsync(Turma entity)
        {
            VerificaDatas(entity.DataInicio, entity.TerminoAula);
            VerificaExistenciaDaTurmaPorNomePeriodoSemestre(entity.Nome, entity.Periodo, entity.Semestre);
            await base.AlterarAsync(entity);
        }
        public void VerificaExistenciaDaTurmaPorNomePeriodoSemestre(string nomeTurma, int periodo, int semestre)
        {
            var turmaFiltrada = _repositoryTurma.VerificarExistenciaDaTurmaPorNomePeriodoSemestre(nomeTurma, periodo, semestre);

            if (turmaFiltrada != null)
            {
                throw new CrudTurmaException($"Já existe uma turma com este nome, vinculada ao {semestre}º semestre do período de {periodo}");
            }
        }




    }
}

﻿using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Repository
{
    public interface IRepositoryMatricula : IRepositoryGeneric<Matricula>
    {
        Task<IList<Matricula>> ObterMatriculasPorAluno(int idAluno);
        Task<List<Matricula>> ObterMatriculasPorTurma(int idTurma);
        Task<int> IncluirMatricula(Matricula matricula);
        Task<List<Matricula>> ObterMatriculaRetidasDoAluno(int alunoID);
    }
}

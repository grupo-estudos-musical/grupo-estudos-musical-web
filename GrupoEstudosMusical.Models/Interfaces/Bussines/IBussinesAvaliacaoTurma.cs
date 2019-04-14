using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesAvaliacaoTurma:IBussinesGeneric<AvaliacaoTurma>
    {
        AvaliacaoTurma ObterPorIds(int turma, int avaliacao);
        List<AvaliacaoTurma> ObterPelaTurma(int turma);
        AvaliacaoTurma ObterPorId(Guid AvaliacaoTurmaID);
        Task AdicionarAvaliacaoAosAlunosDaTurma(List<Matricula> alunosMatriculados, Guid avaliacaoId);
    }
}

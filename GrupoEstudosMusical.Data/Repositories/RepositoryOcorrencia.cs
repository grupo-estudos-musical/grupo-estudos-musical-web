using System;
using System.Collections.Generic;
using System.Linq;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Entities.Relatorios;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryOcorrencia : RepositoryGeneric<Ocorrencia>, IRepositoryOcorrencia
    {
        public List<OcorrenciasParaRelatorio> ObterOcorrenciasParaRelatorio(int AlunoId, int IdOcorrencia)
        {
            //var listaOcorrenciasParaRelatorio = Context.Ocorrencias.Join(Context.Alunos, ocorrencia => ocorrencia.AlunoID, aluno => aluno.Id,
            //    (ocorrencia, aluno) => new { ocorrencia, aluno })
            //    .Join(Context.Turmas, ocorrencia => ocorrencia.ocorrencia.TurmaID, turma => turma.Id,
            //    (ocorrencia, turma) => new { ocorrencia, turma }).Join(Context.Professores, turma => turma.turma.ProfessorID, professor => professor.Id,
            //    (ocorrencia, professor) => new { ocorrencia, professor });
            throw new Exception("R");
        }

        public List<Ocorrencia> ObterOcorrenciasPorAluno(int AlunoId)
        {
            return Context.Ocorrencias.Where(o => o.AlunoID == AlunoId).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Entities.Relatorios;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryOcorrencia : RepositoryGeneric<Ocorrencia>, IRepositoryOcorrencia
    {
        public List<OcorrenciasParaRelatorio> ObterOcorrenciasParaRelatorio(int AlunoId)
        {
            //var listaOcorrenciasParaRelatorio = Context.Ocorrencias.Join(Context.Alunos, ocorrencia => ocorrencia.AlunoID, aluno => aluno.Id,
            //    (ocorrencia, aluno) => new { ocorrencia, aluno })
            //    .Join(Context.Turmas, ocorrencia => ocorrencia.ocorrencia.TurmaID, turma => turma.Id,
            //    (ocorrencia, turma) => new { ocorrencia, turma }).Join(Context.Professores, turma => turma.turma.ProfessorID, professor => professor.Id,
            //    (ocorrencia, professor) => new { ocorrencia, professor });
            var listaOcorrenciasParaRelatorio = new List<OcorrenciasParaRelatorio>();

            var ocorrenciaComDadosMapeados = ObterOcorrenciasPorAluno(AlunoId);

            foreach (var ocorrencia in ocorrenciaComDadosMapeados)
            {
                listaOcorrenciasParaRelatorio.Add (new OcorrenciasParaRelatorio(ocorrencia.Aluno.Nome, ocorrencia.Titulo, ocorrencia.DataOcorrido, ocorrencia.Resumo,
                    ocorrencia.Turma.Professor.Nome, ocorrencia.Id));
            }

            return listaOcorrenciasParaRelatorio;
        }


        //public List<Ocorrencia> ObterOcorrenciasPorAluno(int AlunoId)
        //{
        //    return Context.Ocorrencias.Where(o => o.AlunoID == AlunoId).ToList();
        //}
        public List<Ocorrencia> ObterOcorrenciasPorAluno(int AlunoId)
        {
            return DbSet.Include(o => o.Aluno)
                .Include(o => o.Turma)
                .ThenInclude(t => t.Professor).Where(o => o.AlunoID == AlunoId).ToList();
        }
    }
}

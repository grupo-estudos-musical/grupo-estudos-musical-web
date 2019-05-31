using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Entities.Relatorios;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryOcorrencia : RepositoryGeneric<Ocorrencia>, IRepositoryOcorrencia
    {
        //public List<OcorrenciasParaRelatorio> ObterOcorrenciasParaRelatorio(int AlunoId)
        //{
        //    var listaOcorrenciasParaRelatorio = new List<OcorrenciasParaRelatorio>();

        //    var ocorrenciaComDadosMapeados = ObterOcorrenciasPorAluno(AlunoId);

        //    foreach (var ocorrencia in ocorrenciaComDadosMapeados)
        //    {
        //        listaOcorrenciasParaRelatorio.Add (new OcorrenciasParaRelatorio(ocorrencia.Aluno.Nome, ocorrencia.Titulo, ocorrencia.DataOcorrido, ocorrencia.Resumo,
        //            ocorrencia.Turma.Professor.Nome, ocorrencia.Id));
        //    }

        //    return listaOcorrenciasParaRelatorio;
        //}
        public List<Ocorrencia> ObterOcorrenciasPorAluno(int AlunoId)
        {
            return DbSet.Include(o => o.Aluno)
                .Include(o => o.Turma)
                .ThenInclude(t => t.Professor).Where(o => o.AlunoID == AlunoId).ToList();
        }

        public List<Ocorrencia> ObterOcorrenciasPorTurma(DateTime dataInicial, DateTime dataFinal, int turmaID)
        {
            var listaDeOcorrenciasPorTurma = DbSet.Include(o => o.Aluno)
                .Include(o => o.Turma).Where(o => o.TurmaID == turmaID).ToList();

            if (dataInicial != null && dataFinal != null)
            {
                listaDeOcorrenciasPorTurma = listaDeOcorrenciasPorTurma
                    .Where(o => o.DataOcorrido.Date >= dataInicial.Date && o.DataOcorrido.Date <= dataFinal.Date).ToList();
            }
            return listaDeOcorrenciasPorTurma;
        }

        public override Task<Ocorrencia> ObterPorIdAsync(int id)
        {
            return DbSet.Include(o => o.Aluno)
                .Include(o => o.Turma)
                .ThenInclude(o => o.Professor).Where(o => o.Id == id).FirstAsync();
        }
    }
}

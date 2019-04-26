using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrupoEstudosMusical.Data.Repositories
{
    public class RepositoryInstrumentoDoAluno : RepositoryGeneric<InstrumentoDoAluno>, IRepositoryInstrumentoDoAluno
    {
        public List<InstrumentoDoAluno> ObterInstrumentosDoAluno(int alunoId) =>
            Context.InstrumentoDoAlunos.Include(i => i.Fabricante)
            .Include(i => i.Instrumento)
            .Where(i => i.AlunoID == alunoId).ToList();
        

        public InstrumentoDoAluno ObterPorIdGuid(Guid Id) =>
            Context.InstrumentoDoAlunos.FirstOrDefault(i => i.Id == Id);
    }
}

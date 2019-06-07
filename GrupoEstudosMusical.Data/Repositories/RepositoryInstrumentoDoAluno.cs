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
            .Include(i => i.Inventario)
            .ThenInclude(i => i.Instrumento).Where(i => i.AlunoID == alunoId)
            .Include(i => i.Usuario)
            .ToList();
        

        public InstrumentoDoAluno ObterPorIdGuid(Guid Id) =>
            Context.InstrumentoDoAlunos
            .Include(i => i.Usuario)
            .Include(i => i.Fabricante)
            .Include(i => i.Inventario)
            .ThenInclude(i => i.Instrumento)
            .FirstOrDefault(i => i.IdInstrumentoDoAluno == Id);

        public InstrumentoDoAluno ObterPorAlunoEInventarioGuid(int alunoID, Guid inventarioID) =>
            Context.InstrumentoDoAlunos.FirstOrDefault(i => i.AlunoID == alunoID && i.InventarioID == inventarioID);
    }
}

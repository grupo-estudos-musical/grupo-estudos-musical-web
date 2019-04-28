using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesInstrumentoDoAluno : BussinesGeneric<InstrumentoDoAluno>, IBussinesInstrumentoDoAluno
    {
        readonly IRepositoryInstrumentoDoAluno _repositoryInstrumentoDoAluno;
        public BussinesInstrumentoDoAluno(IRepositoryInstrumentoDoAluno repository) : base(repository)
        {
            _repositoryInstrumentoDoAluno = repository;
        }

        public List<InstrumentoDoAluno> ObterInstrumentosDoAluno(int alunoId) =>
            _repositoryInstrumentoDoAluno.ObterInstrumentosDoAluno(alunoId);

        public InstrumentoDoAluno ObterPorIdGuid(Guid Id) =>
            _repositoryInstrumentoDoAluno.ObterPorIdGuid(Id);
    }
}

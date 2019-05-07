using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Bussines.StaticList;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesInstrumentoDoAluno : BussinesGeneric<InstrumentoDoAluno>, IBussinesInstrumentoDoAluno
    {
        readonly IRepositoryInstrumentoDoAluno _repositoryInstrumentoDoAluno;
        readonly IBussinesInventario _bussinesInventario;
        public BussinesInstrumentoDoAluno(IRepositoryInstrumentoDoAluno repository, IBussinesInventario bussinesInventario) : base(repository)
        {
            _repositoryInstrumentoDoAluno = repository;
            _bussinesInventario = bussinesInventario;
        }

        public List<InstrumentoDoAluno> ObterInstrumentosDoAluno(int alunoId) =>
            _repositoryInstrumentoDoAluno.ObterInstrumentosDoAluno(alunoId);

        public InstrumentoDoAluno ObterPorIdGuid(Guid Id) =>
            _repositoryInstrumentoDoAluno.ObterPorIdGuid(Id);

        public async override Task InserirAsync(InstrumentoDoAluno entity)
        {
            ValidarEmprestimoAluno(entity);
            entity.Status = StatusDeEmprestimoStaticList.Emprestado();  
            await base.InserirAsync(entity);
            await _bussinesInventario.SubtrairOuAdicionarInventario(entity.InventarioID, true);
        }
        void ValidarEmprestimoAluno(InstrumentoDoAluno entity)
        {
            if(ObterPorAlunoEInventarioGuid(entity.AlunoID, entity.InventarioID) != null)
            {
                throw new CrudEmprestimoException("Instrumento já emprestado para este aluno");
            }

            if(string.IsNullOrEmpty(entity.Cor) || string.IsNullOrEmpty(entity.AnoDeFabricacaoInstrumento) || entity.DataEmprestimo == null
                || entity.FabricanteID == Guid.Empty)
            {
                throw new CrudEmprestimoException("O campo cor, ano de fabricação, data de empréstimo e fabricante não podem ser nulos!");
            }
        }

        public InstrumentoDoAluno ObterPorAlunoEInventarioGuid(int alunoID, Guid inventarioID) =>
            _repositoryInstrumentoDoAluno.ObterPorAlunoEInventarioGuid(alunoID, inventarioID);

        public async Task RealizarDevolucao(Guid instrumentoDoAlunoID)
        {
            var instrumentoDoAluno = _repositoryInstrumentoDoAluno.ObterPorIdGuid(instrumentoDoAlunoID);
            if(instrumentoDoAluno == null)
            {
                throw new Exception("Instrumento não encontrado");
            }

            instrumentoDoAluno.Status = StatusDeEmprestimoStaticList.Devolvido();
            await _repositoryInstrumentoDoAluno.AlterarAsync(instrumentoDoAluno);
            await _bussinesInventario.SubtrairOuAdicionarInventario(instrumentoDoAluno.InventarioID, false);
        }
    }
}

using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Bussines.StaticList;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Entities.Relatorios;
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
        readonly IRepositoryUsuario _repositoryUsuario;
        readonly IRepositoryAluno repositoryAluno;
        public BussinesInstrumentoDoAluno(IRepositoryInstrumentoDoAluno repository, IBussinesInventario bussinesInventario, IRepositoryUsuario repositoryUsuario, IRepositoryAluno repositoryAluno) : base(repository)
        {
            _repositoryInstrumentoDoAluno = repository;
            _bussinesInventario = bussinesInventario;
            _repositoryUsuario = repositoryUsuario;
            this.repositoryAluno = repositoryAluno;
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

        public async Task<DetalhesDoEmprestimo> ObterDadosDeEmprestimos(int alunoid)
        {
            var emprestimos = _repositoryInstrumentoDoAluno.ObterInstrumentosDoAluno(alunoid);
            if (emprestimos.Count <= 0)
                throw new Exception("Não há dados a serem apresentados!");
            var aluno = await repositoryAluno.ObterPorIdAsync(alunoid);
            return new DetalhesDoEmprestimo() { Instrumentos = emprestimos, Aluno = aluno };
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

using System;
using System.Threading.Tasks;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesAluno : BussinesGeneric<Aluno>, IBussinesAluno
    {
        private readonly IRepositoryAluno _repositoryAluno;

        public BussinesAluno(IRepositoryAluno repositoryAluno) : base(repositoryAluno)
        {
            _repositoryAluno = repositoryAluno;
        }

        public async override Task AlterarAsync(Aluno entity)
        {
            ValidarContato(entity);
            var alunoCpf = _repositoryAluno.ObterPorCpf(entity.Cpf);
            if (alunoCpf != null && alunoCpf.Id != entity.Id)
                throw new ArgumentException("Já existe um Aluno com o mesmo CPF.");
            await base.AlterarAsync(entity);
        }

        public async override Task InserirAsync(Aluno entity)
        {
            ValidarContato(entity);
            var alunoCpf = _repositoryAluno.ObterPorCpf(entity.Cpf);
            if (alunoCpf != null)
                throw new ArgumentException("Já existe um Aluno com o mesmo CPF.");
            await base.InserirAsync(entity);
        }

        private void ValidarContato(Aluno entity)
        {
            if (entity.Telefone == null && entity.Celular == null)
                throw new ArgumentException("Aluno deve ter ao menos um Telefone para contato.");
        }
    }
}

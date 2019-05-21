using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using GrupoEstudosMusical.Bussines.Helpers;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesAluno : BussinesGeneric<Aluno>, IBussinesAluno
    {
        private readonly IRepositoryAluno _repositoryAluno;
        private readonly IRepositoryUsuario _repositoryUsuario;

        public BussinesAluno(IRepositoryAluno repositoryAluno, IRepositoryUsuario repositoryUsuario) : base(repositoryAluno)
        {
            _repositoryAluno = repositoryAluno;
            _repositoryUsuario = repositoryUsuario;
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

        public async Task<string> InserirAsync(Aluno aluno, Usuario usuario)
        {
            var hash = new HashHelper(SHA512.Create());
            string senha;

            while (true)
            {
                senha = SenhaHelper.GerarSenhaNumericaAleatoria();
                var senhaExistente = await _repositoryUsuario.ObterPorSenhaAsync(hash.CriptografarSenha(senha));
                if (senhaExistente == null)
                    break;
            }

            usuario.Senha = hash.CriptografarSenha(senha);
            await _repositoryUsuario.InserirAsync(usuario);
            aluno.UsuarioId = usuario.Id;
            await InserirAsync(aluno);
            return senha;
        }

        private void ValidarContato(Aluno entity)
        {
            if (entity.Telefone == null && entity.Celular == null)
                throw new ArgumentException("Aluno deve ter ao menos um Telefone para contato.");
        }
    }
}

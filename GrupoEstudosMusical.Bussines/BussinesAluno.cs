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
        private readonly IBussinesUsuario _bussinesUsuario;

        public BussinesAluno(IRepositoryAluno repositoryAluno, IBussinesUsuario bussinesUsuario) : base(repositoryAluno)
        {
            _repositoryAluno = repositoryAluno;
            _bussinesUsuario = bussinesUsuario;
        }

        public async override Task AlterarAsync(Aluno entity)
        {
            ValidarContato(entity);
            var alunoCpf = _repositoryAluno.ObterPorCpf(entity.Cpf);
            if (alunoCpf != null && alunoCpf.Id != entity.Id)
                throw new ArgumentException("Já existe um Aluno com o mesmo CPF.");

            var alunoEmail = _repositoryAluno.ObterPorEmail(entity.Email);
            if (alunoEmail != null && alunoEmail.Id != entity.Id)
                throw new ArgumentException("Já existe um Aluno com o mesmo e-mail.");

            var usuario = await _bussinesUsuario.ObterPorIdAsync(entity.UsuarioId);
            usuario.Email = entity.Email;
            await _bussinesUsuario.AlterarAsync(usuario);

            await base.AlterarAsync(entity);
        }

        public override Task InserirAsync(Aluno entity) => throw new NotImplementedException();

        public async Task<string> InserirAsync(Aluno aluno, Usuario usuario)
        {
            ValidarContato(aluno);
            var alunoCpf = _repositoryAluno.ObterPorCpf(aluno.Cpf);
            if (alunoCpf != null)
                throw new ArgumentException("Já existe um Aluno com o mesmo CPF.");

            var alunoEmail = _repositoryAluno.ObterPorEmail(aluno.Email);
            if (alunoEmail != null)
                throw new ArgumentException("Já existe um Aluno com o mesmo e-mail.");

            var senha = await _bussinesUsuario.Inserir(usuario);

            aluno.UsuarioId = usuario.Id;
            await _repositoryAluno.InserirAsync(aluno);
            return senha;
        }

        public async Task<Aluno> ObterPorUsuario(int idUsuario) => 
            await _repositoryAluno.ObterPorUsuario(idUsuario);

        private void ValidarContato(Aluno entity)
        {
            if (entity.Telefone == null && entity.Celular == null)
                throw new ArgumentException("Aluno deve ter ao menos um Telefone para contato.");
        }
    }
}

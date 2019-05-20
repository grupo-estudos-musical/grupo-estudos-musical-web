using GrupoEstudosMusical.Bussines.Helpers;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesProfessor : BussinesGeneric<Professor>, IBussinesProfessor
    {
        private readonly IRepositoryProfessor _repositoryProfessor;
        private readonly IRepositoryUsuario _repositoryUsuario;

        public BussinesProfessor(IRepositoryProfessor repositoryProfessor, IRepositoryUsuario repositoryUsuario) : base(repositoryProfessor)
        {
            _repositoryProfessor = repositoryProfessor;
            _repositoryUsuario = repositoryUsuario;
        }

        public override Task AlterarAsync(Professor entity)
        {
            ValidarContato(entity);
            return base.AlterarAsync(entity);
        }

        public override Task InserirAsync(Professor entity)
        {
            ValidarContato(entity);
            return base.InserirAsync(entity);
        }

        public async Task<string> InserirAsync(Professor professor, Usuario usuario)
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
            professor.UsuarioId = usuario.Id;
            await InserirAsync(professor);
            return senha;
        }

        private void ValidarContato(Professor professor)
        {
            if (professor.Telefone == null && professor.Celular == null)
                throw new ArgumentException("Professor deve ter ao menos um Telefone para contato.");
        }
    }
}

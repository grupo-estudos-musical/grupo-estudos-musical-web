using GrupoEstudosMusical.Bussines.Helpers;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesUsuario : BussinesGeneric<Usuario>, IBussinesUsuario
    {
        private readonly IRepositoryUsuario _repositoryUsuario;

        public BussinesUsuario(IRepositoryUsuario repositoryUsuario) : base(repositoryUsuario)
        {
            _repositoryUsuario = repositoryUsuario;
        }

        public async Task AlterarSenha(int idUsuario, string senha)
        {
            var usuario = await _repositoryUsuario.ObterPorIdAsync(idUsuario);

            var hash = new HashHelper(SHA512.Create());
            var senhaCriptografada = hash.CriptografarSenha(senha);

            usuario.Senha = senhaCriptografada;

            await _repositoryUsuario.AlterarAsync(usuario);
        }

        public async Task<Usuario> AutenticarAsync(string email, string senha)
        {
            var hash = new HashHelper(SHA512.Create());
            var senhaCriptografada = hash.CriptografarSenha(senha);

            return await _repositoryUsuario.AutenticarAsync(email, senhaCriptografada);
        }

        public async Task<string> Inserir(Usuario usuario)
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
            return senha;
        }

        public override Task InserirAsync(Usuario entity) => throw new NotImplementedException();
    }
}

using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesProfessor : BussinesGeneric<Professor>, IBussinesProfessor
    {
        private readonly IRepositoryProfessor _repositoryProfessor;
        private readonly IBussinesUsuario _bussinesUsuario;

        public BussinesProfessor(IRepositoryProfessor repositoryProfessor, IBussinesUsuario bussinesUsuario) : base(repositoryProfessor)
        {
            _repositoryProfessor = repositoryProfessor;
            _bussinesUsuario = bussinesUsuario;
        }

        public override async Task AlterarAsync(Professor entity)
        {
            ValidarContato(entity);

            var professorEmail = await _repositoryProfessor.ObterPorEmail(entity.Email);
            if (professorEmail != null && professorEmail.Id != entity.Id)
                throw new ArgumentException("Já existe um Professor com o mesmo e-mail.");

            var usuario = await _bussinesUsuario.ObterPorIdAsync(entity.UsuarioId);
            usuario.Email = entity.Email;
            await _bussinesUsuario.AlterarAsync(usuario);

            await base.AlterarAsync(entity);
        }

        public override Task InserirAsync(Professor entity) => throw new NotImplementedException();

        public async Task<string> InserirAsync(Professor professor, Usuario usuario)
        {
            ValidarContato(professor);

            var professorEmail = await _repositoryProfessor.ObterPorEmail(professor.Email);
            if (professorEmail != null)
                throw new ArgumentException("Já existe um Professor com o mesmo e-mail.");

            var senha = await _bussinesUsuario.Inserir(usuario);

            professor.UsuarioId = usuario.Id;
            await _repositoryProfessor.InserirAsync(professor);
            return senha;
        }

        private void ValidarContato(Professor professor)
        {
            if (professor.Telefone == null && professor.Celular == null)
                throw new ArgumentException("Professor deve ter ao menos um Telefone para contato.");
        }
    }
}

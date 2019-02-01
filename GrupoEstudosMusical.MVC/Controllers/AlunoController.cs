using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IBussinesAluno bussinesAluno;

        public AlunoController(IBussinesAluno bussinesAluno)
        {
            this.bussinesAluno = bussinesAluno;
        }

        // GET: Aluno
        public async Task<ActionResult> Index()
        {

            var alunos = await bussinesAluno.ObterTodosAsync();
            await bussinesAluno.InserirAsync(new Aluno
            {
                Nome = "testye",
                Bairro = "teste",
                Celular = "teste",
                Cep = "teste",
                Cidade = "teste",
                Complemento = "teste",
                Cpf = "teste",
                DataCadastro = DateTime.Now,
                DataNascimento = DateTime.Now,
                Email = "teste",
                Endereco = "teste",
                Rg = "teste",
                Telefone = "teste",
                Uf = "teste"
            });

            var aluno = await bussinesAluno.ObterPorIdAsync(2);

            aluno.Nome = "Nicolas";
            await bussinesAluno.AlterarAsync(aluno);
            await bussinesAluno.DeletarAsync(aluno);

            return View();
        }
    }
}
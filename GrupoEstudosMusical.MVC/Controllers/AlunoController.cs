
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
            
            return View();
        }
    }
}
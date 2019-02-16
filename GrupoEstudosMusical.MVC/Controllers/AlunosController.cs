using AutoMapper;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IBussinesAluno _bussinesAluno;

        public AlunosController(IBussinesAluno bussinesAluno)
        {
            _bussinesAluno = bussinesAluno;            
        }

        public async Task<ActionResult> Index()
        {
            var alunosViewModel = Mapper.Map<IList<Aluno>, IList<AlunoVM>>(await _bussinesAluno.ObterTodosAsync());
            return View(alunosViewModel);
        }

        public ActionResult Novo()
        {
            return View(new AlunoVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(AlunoVM alunoVM)
        {
            try
            {
                var alunoModel = Mapper.Map<AlunoVM, Aluno>(alunoVM);
                await _bussinesAluno.InserirAsync(alunoModel);
                TempData["Mensagem"] = "Aluno cadastrado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(alunoVM);
            }
        }

        public async Task<ActionResult> Editar(int id)
        {
            var alunoVM = Mapper.Map<Aluno, AlunoVM>(await _bussinesAluno.ObterPorIdAsync(id));
            if (alunoVM == null)
            {
                return HttpNotFound();
            }
            return View(alunoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(AlunoVM alunoVM)
        {
            try
            {
                var alunoModel = Mapper.Map<AlunoVM, Aluno>(alunoVM);
                await _bussinesAluno.AlterarAsync(alunoModel);
                TempData["Mensagem"] = "Aluno alterado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(alunoVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deletar(FormCollection formCollection)
        {
            int.TryParse(formCollection["id"], out int id);
            var alunoModel = await _bussinesAluno.ObterPorIdAsync(id);
            await _bussinesAluno.DeletarAsync(alunoModel);
            TempData["Mensagem"] = "Aluno apagado com sucesso.";
            return RedirectToAction(nameof(Index));
        }
    }
}
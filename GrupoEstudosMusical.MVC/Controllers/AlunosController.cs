using AutoMapper;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Enums;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Helpers;
using GrupoEstudosMusical.MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            ViewBag.TiposResponsaveis = Enum.GetValues(typeof(TipoResponsavelEnum));
            return View(new AlunoVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(AlunoVM alunoVM)
        {
            try
            {
                alunoVM.Id = (await _bussinesAluno.ObterUltimoIdAsync()) + 1;
                alunoVM.ImagemUrl = AlunoHelper.ObterNomeArquivo(alunoVM);
                var alunoModel = Mapper.Map<AlunoVM, Aluno>(alunoVM);

                await _bussinesAluno.InserirAsync(alunoModel);
                AlunoHelper.SalvarImagemAluno(alunoVM.ImagemUpload, AlunoHelper.ObterCaminhoImagemAluno(alunoVM, Server));

                return RedirectToRoute("Matricular", new { controller = "Matriculas", idAluno = alunoVM.Id });
            }
            catch (ArgumentException ex)
            {
                ViewBag.TiposResponsaveis = Enum.GetValues(typeof(TipoResponsavelEnum));
                TempData["Mensagem"] = ex.Message;
                return View(alunoVM);
            }
        }

        [Route("Alunos/Editar/{id}")]
        public async Task<ActionResult> Editar(int id, string returnUrl = "")
        {
            var alunoVM = Mapper.Map<Aluno, AlunoVM>(await _bussinesAluno.ObterPorIdAsync(id));
            if (alunoVM == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.TiposResponsaveis = Enum.GetValues(typeof(TipoResponsavelEnum));
            return View(alunoVM);
        }

        public async Task<ActionResult> VisaoGeral(int Id)
        {
            var obterDadosDoAlunoVm = Mapper.Map<Aluno, AlunoVM>(await _bussinesAluno.ObterPorIdAsync(Id));
            if(obterDadosDoAlunoVm == null)
                return HttpNotFound();
            return View(obterDadosDoAlunoVm);
        }

        [Route("Alunos/Editar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(AlunoVM alunoVM, string returnUrl = "")
        {
            try
            {
                if (alunoVM.ImagemUpload != null)
                {
                    alunoVM.ImagemUrl = AlunoHelper.ObterNomeArquivo(alunoVM);
                    AlunoHelper.SalvarImagemAluno(alunoVM.ImagemUpload, AlunoHelper.ObterCaminhoImagemAluno(alunoVM, Server));
                }
                var alunoModel = Mapper.Map<AlunoVM, Aluno>(alunoVM);
                await _bussinesAluno.AlterarAsync(alunoModel);

                if (string.IsNullOrEmpty(returnUrl))
                {
                    TempData["Mensagem"] = "Aluno alterado com sucesso.";
                    return RedirectToAction(nameof(Index));
                }

                return Redirect(returnUrl);
            }
            catch (ArgumentException ex)
            {
                ViewBag.ReturnUrl = returnUrl;
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
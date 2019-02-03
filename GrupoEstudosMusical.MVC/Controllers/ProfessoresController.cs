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
    public class ProfessoresController : Controller
    {
        private readonly IBussinesProfessor _bussinesProfessor;

        public ProfessoresController(IBussinesProfessor bussinesProfessor)
        {
            _bussinesProfessor = bussinesProfessor;
        }

        public async Task<ActionResult> Index()
        {
            var professores = await _bussinesProfessor.ObterTodosAsync();
            var professoresVM = Mapper.Map<IList<Professor>, IList<ProfessorVM>>(professores);
            return View(professoresVM);
        }

        public ActionResult Novo()
        {
            return View(new ProfessorVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(ProfessorVM professorVM)
        {
            try
            {
                var professorModel = Mapper.Map<ProfessorVM, Professor>(professorVM);
                await _bussinesProfessor.InserirAsync(professorModel);
                TempData["Mensagem"] = "Professor Cadastrado com Sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(professorVM);
            }
        }

        public async Task<ActionResult> Editar(int id)
        {
            var professorVM = Mapper.Map<Professor, ProfessorVM>(await _bussinesProfessor.ObterPorIdAsync(id));
            if (professorVM == null)
            {
                return HttpNotFound();
            }
            return View(professorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(ProfessorVM professorVM)
        {
            try
            {
                var professorModel = Mapper.Map<ProfessorVM, Professor>(professorVM);
                await _bussinesProfessor.AlterarAsync(professorModel);
                TempData["Mensagem"] = "Professor Alterado com Sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(professorVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deletar(FormCollection formCollection)
        {
            int.TryParse(formCollection["id"].ToString(), out var id);
            var professorModel = await _bussinesProfessor.ObterPorIdAsync(id);
            await _bussinesProfessor.DeletarAsync(professorModel);
            TempData["Mensagem"] = "Professor Apagado com Sucesso.";
            return RedirectToAction(nameof(Index));
        }
    }
}
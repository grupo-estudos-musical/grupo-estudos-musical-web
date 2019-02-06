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
    public class TurmasController : Controller
    {
        private readonly IBussinesTurma _bussinesTurma;
        private readonly IBussinesModulo _bussinesModulo;
        private readonly IBussinesProfessor _bussinesProfessor;
        public TurmasController(IBussinesTurma bussinesTurma, IBussinesModulo bussinesModulo, IBussinesProfessor bussinesProfessor)
        {
            _bussinesTurma = bussinesTurma;
            _bussinesModulo = bussinesModulo;
            _bussinesProfessor = bussinesProfessor;
        }
        // GET: Turmas
        public async Task<ActionResult> Index()
        {
            var turmasViewModel = Mapper.Map<IList<Turma>, IList<TurmaVM>>(await _bussinesTurma.ObterTodosAsync());
            
            return View(turmasViewModel);

        }
        
        public async Task<ActionResult> Novo()
        {
            await InicializarViewBagAsync();
            return View(new TurmaVM());
        }

        public async Task<ActionResult> Editar(int Id)
        {
            await InicializarViewBagAsync();
            var turma = Mapper.Map<Turma, TurmaVM>(await _bussinesTurma.ObterPorIdAsync(Id));

            if (turma == null)
            {
                return HttpNotFound();
            }
            return View(turma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(TurmaVM turma)
        {
            await InicializarViewBagAsync();
            try
            {
                var turmaModel = Mapper.Map<TurmaVM, Turma>(turma);
                await _bussinesTurma.AlterarAsync(turmaModel);
                TempData["Mensagem"] = "Turma alterada com sucessso";
                return RedirectToAction(nameof(Index));
            }catch(ArgumentException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(turma);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(TurmaVM entity)
        {
            await InicializarViewBagAsync();
            try
            {
                var turmaModel = Mapper.Map<TurmaVM, Turma>(entity);
                await _bussinesTurma.InserirAsync(turmaModel);
                TempData["Mensagem"] = "Turma cadastrada com sucesso";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(entity);
            }
        }

        public async Task InicializarViewBagAsync()
        {
            var modulosViewModel = Mapper.Map<IList<Modulo>, IList<ModuloVM>>(await _bussinesModulo.ObterTodosAsync());
            var professoresViewModel = Mapper.Map<IList<Professor>, IList<ProfessorVM>>(await _bussinesProfessor.ObterTodosAsync());
            ViewBag.Modulos = modulosViewModel;
            ViewBag.Professores = professoresViewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deletar(FormCollection formCollection)
        {
            int.TryParse(formCollection["Id"].ToString(), out var id);
            var turmaModel = await _bussinesTurma.ObterPorIdAsync(id);
            await _bussinesTurma.DeletarAsync(turmaModel);
            TempData["Mensagem"] = "Turma Apagada com Sucesso.";
            return RedirectToAction(nameof(Index));
        }





    }
}
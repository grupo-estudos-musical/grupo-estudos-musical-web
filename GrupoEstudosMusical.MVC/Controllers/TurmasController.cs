using AutoMapper;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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

            //var re = this.RetornarTurmasComInformacoesCompletas(turmasViewModel);

            
            
            return View(turmasViewModel);

        }
        public async Task<IEnumerable<TurmaVM>> RetornarTurmasComInformacoesCompletas(IEnumerable<TurmaVM> turmas)
        {
            List<TurmaVM> lista = new List<TurmaVM>();
            foreach (var turma in turmas)
            {
                turma.NomeProfessor = Mapper.Map<Professor, ProfessorVM>( await _bussinesProfessor.ObterPorIdAsync(turma.ProfessorID)).Nome;
                //turma.NomeModulo = Mapper.Map<Modulo, ModuloVM>(await _bussinesModulo.ObterPorIdAsync(turma.ModuloId)).Nome;
                 lista.Add(turma);
            }
            return lista;
        }
        public async Task<ActionResult> Novo()
        {
            await InicializarViewBagAsync();
            return View(new TurmaVM());
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





    }
}
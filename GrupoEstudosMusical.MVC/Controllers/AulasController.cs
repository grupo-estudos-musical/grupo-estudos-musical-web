using AutoMapper;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class AulasController : Controller
    {
        private readonly IBussinesAula _bussinesAula;
        private readonly IBussinesTurma _bussinesTurma;
        private readonly IBussinesAvaliacaoTurma _bussinesAvaliacaoTurma;

        public AulasController(IBussinesAula bussinesAula, IBussinesTurma bussinesTurma,
            IBussinesAvaliacaoTurma bussinesAvaliacaoTurma)
        {
            _bussinesAula = bussinesAula;
            _bussinesTurma = bussinesTurma;
            _bussinesAvaliacaoTurma = bussinesAvaliacaoTurma;
        }

        public async Task<ActionResult> Index(int idTurma)
        {
            var aulasModel = await _bussinesAula.ObterPorTurma(idTurma);
            var aulasVM = Mapper.Map<List<Aula>, List<AulaVM>>(aulasModel);
            return View(aulasVM);
        }

        public async Task<ActionResult> Novo(int idTurma)
        {
            var turmaVM = Mapper.Map<Turma, TurmaVM>(await _bussinesTurma.ObterPorIdAsync(idTurma));
            var avaliacoesTurmaVM = 
                Mapper.Map<List<AvaliacaoTurma>, List<AvaliacaoTurmaVM>>(_bussinesAvaliacaoTurma.ObterPorTurma(idTurma));
            var aulaVM = new AulaVM
            {
                TurmaId = turmaVM.Id,
                Turma = turmaVM,
                AvaliacoesTurma = avaliacoesTurmaVM
            };
            return View(aulaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(AulaVM aulaVM)
        {
            try
            {
                var aulaModel = Mapper.Map<AulaVM, Aula>(aulaVM);
                var avaliacoesSelecionadas = aulaVM.AvaliacoesTurma.Where(a => a.Selecionado).ToList();
                var avaliacoesTurma = Mapper.Map<List<AvaliacaoTurmaVM>, List<AvaliacaoTurma>>(avaliacoesSelecionadas);
                await _bussinesAula.InserirAsync(aulaModel, avaliacoesTurma);
                return RedirectToAction("Index", "Turmas");
            }
            catch (ArgumentException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(aulaVM);
            }
        }
    }
}
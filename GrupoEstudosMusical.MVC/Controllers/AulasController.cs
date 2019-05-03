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

        public AulasController(IBussinesAula bussinesAula, 
            IBussinesTurma bussinesTurma,
            IBussinesAvaliacaoTurma bussinesAvaliacaoTurma)
        {
            _bussinesAula = bussinesAula;
            _bussinesTurma = bussinesTurma;
            _bussinesAvaliacaoTurma = bussinesAvaliacaoTurma;
        }

        public async Task<ActionResult> Index(int idTurma)
        {
            var turmaVM = Mapper.Map<Turma, TurmaVM>(await _bussinesTurma.ObterPorIdAsync(idTurma));
            if (turmaVM == null)
                return HttpNotFound();

            var aulasModel = await _bussinesAula.ObterPorTurma(idTurma);
            var aulasVM = Mapper.Map<List<Aula>, List<AulaVM>>(aulasModel);

            var listaAulaVM = new ListaAulasVM
            {
                Turma = turmaVM,
                Aulas = aulasVM
            };
            return View(listaAulaVM);
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
                TempData["mensagem"] = "Aula cadastrada com sucesso!";
                TempData["tipo"] = "success";
                return RedirectToAction("Index", "Aulas", new { idTurma = aulaVM.TurmaId });
            }
            catch (ArgumentException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(aulaVM);
            }
        }

        public async Task<ActionResult> Detalhes(int id)
        {
            var aulaModel = await _bussinesAula.ObterPorIdAsync(id);
            var aulaVM = Mapper.Map<Aula, AulaVM>(aulaModel);
            return View(aulaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deletar(int id)
        {
            var aulaModel = await _bussinesAula.ObterPorIdAsync(id);
            if (aulaModel == null)
                return HttpNotFound();
            await _bussinesAula.DeletarAsync(aulaModel);
            TempData["mensagem"] = "Aula apagada com sucesso!";
            TempData["tipo"] = "success";
            return RedirectToAction(nameof(Index), new { idTurma = aulaModel.TurmaId });
        }
    }
}
using AutoMapper;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class ChamadasController : Controller
    {
        private readonly IBussinesChamada _bussinesChamada;
        private readonly IBussinesTurma _bussinesTurma;
        private readonly IBussinesMatricula _bussinesMatricula;      

        public ChamadasController(IBussinesChamada bussinesChamada, IBussinesTurma bussinesTurma, IBussinesMatricula bussinesMatricula)
        {
            _bussinesChamada = bussinesChamada;
            _bussinesTurma = bussinesTurma;
            _bussinesMatricula = bussinesMatricula;
        }

        public async Task<ActionResult> Index(int idTurma)
        {
            var listaChamadas = new ListaChamadasVM();
            listaChamadas.Turma = Mapper.Map<Turma, TurmaVM>(await _bussinesTurma.ObterPorIdAsync(idTurma));

            var chamadasVM = Mapper.Map<IList<Chamada>, List<ChamadaVM>>(await _bussinesChamada.ObterChamadasPorTurmaAsync(idTurma));
            listaChamadas.Chamadas = chamadasVM;

            return View(listaChamadas);
        }

        public async Task<ActionResult> Novo(int idTurma)
        {
            var chamadaVM = new ChamadaVM();
            chamadaVM.IdTurma = idTurma;
            chamadaVM.Turma = Mapper.Map<Turma, TurmaVM>(await _bussinesTurma.ObterPorIdAsync(idTurma));
            chamadaVM.Turma.Matriculas = await _bussinesMatricula.ObterMatriculasPorTurma(idTurma);
            return View(chamadaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(ChamadaVM chamadaVM, IEnumerable<FrequenciaVM> frequenciaVMs)
        {
            chamadaVM.Turma = null;
            var chamadaModel = Mapper.Map<ChamadaVM, Chamada>(chamadaVM);
            var frequenciasModel = Mapper.Map<IEnumerable<FrequenciaVM>, List<Frequencia>>(frequenciaVMs);
            chamadaModel.Frequencias = frequenciasModel;
            await _bussinesChamada.InserirAsync(chamadaModel);
            TempData["mensagem"] = "Chamada relizada com sucesso.";
            TempData["tipo"] = "success";
            return RedirectToAction(nameof(Index), "Chamadas", new { idTurma = chamadaVM.IdTurma});
        }

        public async Task<ActionResult> Detalhes(int id)
        {
            var chamadaVM = Mapper.Map<Chamada, ChamadaVM>(await _bussinesChamada.ObterPorIdAsync(id));
            if (chamadaVM == null)
                return HttpNotFound();
            return View(chamadaVM);
        }
    }
}
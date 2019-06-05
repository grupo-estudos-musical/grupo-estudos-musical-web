using AutoMapper;
using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.App_Start;
using GrupoEstudosMusical.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    [AuthorizeGem]
    public class TurmasController : Controller
    {
        private readonly IBussinesTurma _bussinesTurma;
        private readonly IBussinesModulo _bussinesModulo;
        private readonly IBussinesProfessor _bussinesProfessor;
        private readonly IBussinesAvaliacaoTurma _bussinesAvaliacaoTurma;
        private readonly IBussinesPalhetaDeNotas _bussinesPalhetaDeNotas;
        public TurmasController(IBussinesTurma bussinesTurma, IBussinesModulo bussinesModulo, IBussinesProfessor bussinesProfessor,
            IBussinesAvaliacaoTurma bussinesAvaliacaoTurma, IBussinesPalhetaDeNotas bussinesPalhetaDeNotas)
        {
            _bussinesTurma = bussinesTurma;
            _bussinesModulo = bussinesModulo;
            _bussinesProfessor = bussinesProfessor;
            _bussinesAvaliacaoTurma = bussinesAvaliacaoTurma;
            _bussinesPalhetaDeNotas = bussinesPalhetaDeNotas;
        }
        // GET: Turmas
        public async Task<ActionResult> Index()
        {
            var turmasViewModel = Mapper.Map<IList<Turma>, IList<TurmaVM>>(await _bussinesTurma.ObterTodosAsync());
            
            return View(turmasViewModel);

        }

        

        public async Task<ActionResult> VisaoGeral(int Id)
        {
            var obterDadosDaTurma = Mapper.Map<Turma, TurmaVM>(await _bussinesTurma.ObterPorIdAsync(Id));
            if (obterDadosDaTurma == null)
                return HttpNotFound();
            if (obterDadosDaTurma.Status == "Encerrada")
                return HttpNotFound("Turma encerrada!");
            return View(obterDadosDaTurma);
        }



        [HttpPost]
        public async Task<JsonResult> FinalizarVigenciaTurma(int ID)
        {
            try
            {
                await _bussinesTurma.FinalizarVigencia(ID);
                return Json(new { result = true, mensagem = "Turma Finalizada, \n a situação da matrícula dos alunos foram atualizadas" }, JsonRequestBehavior.AllowGet);
            }catch(Exception ex)
            {
                return Json(new { result = false, mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Novo()
        {
            await InicializarViewBagAsync();
            return View(new TurmaVM());
        }


        public async Task<ActionResult> AlunosDaTurma(int ID)
        {
            
            var turma = await _bussinesTurma.ObterPorIdAsync(ID);
            if (turma == null)
                return HttpNotFound("Turma não encontrada");
            ViewBag.Turma = turma;
            return View(turma.Matriculas);
        }
        [HttpGet]
        public ActionResult ObterPalhetasDeNotasPorAvaliacaoEhTurma(string AvaliacaoID, int TurmaID)
        {
            var palhetasDeNotas = _bussinesPalhetaDeNotas.ObterPalhetasPorAvaliacaoEhTurma(Guid.Parse(AvaliacaoID), TurmaID);
            return PartialView("_PalhetaDeNotas", palhetasDeNotas);
        }
        
        [HttpPost]
        public async Task<JsonResult> LancarNotaDoAluno(double nota, Guid palhetaID)
        {
            try
            {
                var palhetaDeNota = _bussinesPalhetaDeNotas.ObterPorId(palhetaID);
                palhetaDeNota.Nota = nota;
                await _bussinesPalhetaDeNotas.AlterarAsync(palhetaDeNota);
                return Json(new { result = true, mensagem = "Notas lançadas com sucesso!" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new { result = false, mensagem = "Ocorreu um erro!" }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public async Task<JsonResult> RecalculoAcademico(int Id)
        {
            try
            {
                await _bussinesTurma.RecalculoAcademico(Id);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public async Task<ActionResult> LancarNotas(int Id)
        {
            var turma = Mapper.Map<Turma,TurmaVM>( await _bussinesTurma.ObterPorIdAsync(Id));
            ViewBag.AvaliacoesTurma = Mapper.Map<IList<AvaliacaoTurma>, IList<AvaliacaoTurmaVM>>(_bussinesAvaliacaoTurma.ObterPorTurma(Id));
            return View(turma);
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
            }catch(CrudTurmaException ex)
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
            }catch(CrudTurmaException ex)
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

        [HttpGet]
        public ActionResult TurmasAtivas(int moduloId, int alunoId)
        {
            try
            {
                
                var turmas = _bussinesTurma.ObterTurmasAtivasPorModulo(moduloId).ToList();
                var turmasMatricula = new List<TurmasMatriculaVM>();
                turmas.ForEach(turma =>
                {
                    turmasMatricula.Add(new TurmasMatriculaVM
                    {
                        Id = turma.Id,
                        Nome = turma.Nome,
                        QuantidadeAlunos = turma.QuantidadeAlunos,
                        QuantidadeMatriculas = turma.Matriculas.Count,
                        AlunoMatriculado = turma.Matriculas.Any(m => m.AlunoId == alunoId)
                    });
                });
                return Json(turmasMatricula, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

    }
}
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using GrupoEstudosMusical.Models.Entities;
using AutoMapper;
using System.Collections.Generic;
using GrupoEstudosMusical.MVC.Models;
using GrupoEstudosMusical.Models.Enums;
using GrupoEstudosMusical.MVC.App_Start;

namespace GrupoEstudosMusical.MVC.Controllers
{
    [AuthorizeGem]
    public class OcorrenciasController : Controller
    {
        readonly IBussinesOcorrencia _bussinesOcorrencia;
        readonly IBussinesTurma _bussinesTurma;
        readonly IBussinesMatricula _bussinesMatricula;
        public OcorrenciasController(IBussinesOcorrencia bussinesOcorrencia, IBussinesTurma bussinesTurma, IBussinesMatricula bussinesMatricula)
        {
            _bussinesOcorrencia = bussinesOcorrencia;
            _bussinesTurma = bussinesTurma;
            _bussinesMatricula = bussinesMatricula;
        }
        // GET: Ocorrencias
        public ActionResult Index(int AlunoId)
        {
            var ocorrenciasDoAlunoVm = Mapper.Map<IList<Ocorrencia>, IList<OcorrenciaVM>>(_bussinesOcorrencia.ObterOcorrenciasPorAluno(AlunoId));
            if (ocorrenciasDoAlunoVm.Count == 0)
            {
                TempData["Mensagem"] = "Este aluno não possui ocorrência!";
                return RedirectToAction("VisaoGeral", "Alunos", new { Id = AlunoId });
            }
            ViewBag.IDALUNO = AlunoId;
            return View(ocorrenciasDoAlunoVm);
        }

        public async Task<ActionResult> Editar(int Id)
        {
            await InicializarViewBagAsync();
            var ocorrenciaVm = Mapper.Map<Ocorrencia, OcorrenciaVM>(await _bussinesOcorrencia.ObterPorIdAsync(Id));
            
            if (ocorrenciaVm == null)
            {
                return HttpNotFound();
            }
            return View(ocorrenciaVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(OcorrenciaVM ocorrencia)
        {
            await InicializarViewBagAsync();
            try
            {
                var ocorrenciaModel = Mapper.Map<OcorrenciaVM, Ocorrencia>(ocorrencia);
                await _bussinesOcorrencia.AlterarAsync(ocorrenciaModel);
                TempData["Mensagem"] = "Alteração realizada com sucesso";
                return RedirectToAction("Index", new { AlunoId = ocorrenciaModel.AlunoID });
            }
            catch (Exception ex)
            {
                throw new Exception("Tratar");
                
            }
        }

        public async Task<ActionResult> Novo(int AlunoId)
        {
            await InicializarViewBagAsync();
            ViewBag.Turmas = await _bussinesTurma.ObterTurmasDoAluno(AlunoId);
            

            return View(new OcorrenciaVM() { AlunoID = AlunoId });
        }
        [HttpPost]
        public async Task<ActionResult> Novo(OcorrenciaVM ocorrencia)
        {
            await InicializarViewBagAsync();
            var ocorrenciaModel = Mapper.Map<OcorrenciaVM, Ocorrencia>(ocorrencia);

            await _bussinesOcorrencia.InserirAsync(ocorrenciaModel);

            return RedirectToAction("Index", new { AlunoId = ocorrencia.AlunoID });

        }


        public async Task InicializarViewBagAsync()
        {
            var turmasViewModel = Mapper.Map<IList<Turma>, IList<TurmaVM>>(await _bussinesTurma.ObterTodosAsync());
            ViewBag.Turmas = turmasViewModel;
            ViewBag.Tipos = Enum.GetValues(typeof(TiposOcorrenciaEnum));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deletar(FormCollection formCollection)
        {
            int.TryParse(formCollection["Id"], out int Id);
            var ocorrenciaModel = await _bussinesOcorrencia.ObterPorIdAsync(Id);
            await _bussinesOcorrencia.DeletarAsync(ocorrenciaModel);
            TempData["Mensagem"] = "Ocorrência apagada com sucesso.";
            return RedirectToAction(nameof(Index), new { AlunoId = ocorrenciaModel.AlunoID });
        }

    }
}
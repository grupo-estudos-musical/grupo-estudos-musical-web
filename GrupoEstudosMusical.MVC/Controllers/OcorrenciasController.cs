using GrupoEstudosMusical.Models.Interfaces.Bussines;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using GrupoEstudosMusical.Models.Entities;
using AutoMapper;
using System.Collections.Generic;
using GrupoEstudosMusical.MVC.Models;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class OcorrenciasController : Controller
    {
        readonly IBussinesOcorrencia _bussinesOcorrencia;
        readonly IBussinesTurma _bussinesTurma;

        public OcorrenciasController(IBussinesOcorrencia bussinesOcorrencia, IBussinesTurma bussinesTurma)
        {
            _bussinesOcorrencia = bussinesOcorrencia;
            _bussinesTurma = bussinesTurma;
        }
        // GET: Ocorrencias
        public ActionResult Index(int AlunoId)
        {
            var ocorrenciasDoAlunoVm = Mapper.Map<IList<Ocorrencia>, IList<OcorrenciaVM>>(_bussinesOcorrencia.ObterOcorrenciasPorAluno(AlunoId));
            if (ocorrenciasDoAlunoVm.Count == 0)
            {
                TempData["Mensagem"] = "Este aluno não possui nenhuma ocorrência!";
                return RedirectToAction("VisaoGeral", "Alunos", new { Id = AlunoId });
            }
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
                return View(ocorrencia);
            }
        }

        public async Task<ActionResult> Novo(int AlunoId)
        {
            await InicializarViewBagAsync();

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
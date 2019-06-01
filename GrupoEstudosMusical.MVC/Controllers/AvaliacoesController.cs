
using AutoMapper;
using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.App_Start;
using GrupoEstudosMusical.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    [AuthorizeGem]
    public class AvaliacoesController : Controller
    {
        // GET: Avaliacao
        public AvaliacoesController(IBussinesAvaliacao bussinesAvaliacao
            , IBussinesAvaliacaoTurma bussinesAvaliacaoTurma, IBussinesTurma bussinesTurma)
        {
            this._bussinesAvaliacao = bussinesAvaliacao;
            this._bussinesTurma = bussinesTurma;
            this._bussinesAvaliacaoTurma = bussinesAvaliacaoTurma;
        }
        readonly IBussinesAvaliacao _bussinesAvaliacao;
        readonly IBussinesAvaliacaoTurma _bussinesAvaliacaoTurma;
        readonly IBussinesTurma _bussinesTurma;
        public async Task<ActionResult> Index()
        {
            var avaliacoes = await _bussinesAvaliacao.ObterTodosAsync();
            var avaliacoesVM = Mapper.Map<IList<Avaliacao>, IList<AvaliacaoVM>>(avaliacoes);
            return View(avaliacoesVM);
        }


        public ActionResult Novo()
        {
            return View(new AvaliacaoVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(AvaliacaoVM avaliacaoVM)
        {
            try
            {
                var modelAvaliacao = Mapper.Map<AvaliacaoVM, Avaliacao>(avaliacaoVM);
                await _bussinesAvaliacao.InserirAsync(modelAvaliacao);
                return RedirectToAction("Index");

                
            }catch(CrudAvaliacaoException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(avaliacaoVM);
            }
        }

        public async Task<ActionResult> Editar(int Id)
        {
            var avaliacaoVM = Mapper.Map<Avaliacao, AvaliacaoVM>(await _bussinesAvaliacao.ObterPorIdAsync(Id));
            return View(avaliacaoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(AvaliacaoVM avaliacao)
        {
            try
            {
                var modelAvaliacao = Mapper.Map<AvaliacaoVM, Avaliacao>(avaliacao);
                await _bussinesAvaliacao.AlterarAsync(modelAvaliacao);

                TempData["Mensagem"] = "Alteração realizada com sucesso";
                return RedirectToAction("Index");
            }
            catch (CrudAvaliacaoException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(avaliacao);
            }   
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deletar(FormCollection formCollection)
        {
            int.TryParse(formCollection["Id"], out int Id);
            var ocorrenciaModel = await _bussinesAvaliacao.ObterPorIdAsync(Id);
            await _bussinesAvaliacao.DeletarAsync(ocorrenciaModel);
            TempData["Mensagem"] = "Avaliação apagada com sucesso.";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AvaliacoesDaTurma(int Id, string NomeTurma)
        {
            if(TempData["MensagemSucesso"] != null)
            {
                ViewBag.MensagemSucesso = TempData["MensagemSucesso"];
            }
            var avaliacoesVM = Mapper.Map<IList<Avaliacao>, IList<AvaliacaoVM>>(await _bussinesAvaliacao.ObterTodosAsync());
            ViewBag.Model = new AvaliacaoTurmaVM() { TurmaID = Id, AvaliacoesDisponiveis = avaliacoesVM.ToList(), NomeTurma = NomeTurma };
            var avaliacoes = Mapper.Map<IList<AvaliacaoTurma>, IList<AvaliacaoTurmaVM>>(_bussinesAvaliacaoTurma.ObterPorTurma(Id));
            ViewBag.NomeTurma = NomeTurma;
            ViewBag.IdTurma = Id;
            return View(avaliacoes);
        }



        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AdicionarAvaliacaoNaTurma(AvaliacaoTurmaVM avaliacao)
        {
            try
            {
                var avaliacaoTurmaModel = Mapper.Map<AvaliacaoTurmaVM, AvaliacaoTurma>(avaliacao);
                var idAvaliacaoDaTurma = SetarIdAvaliacaDaTurma(avaliacaoTurmaModel);
                await _bussinesAvaliacaoTurma.InserirAsync(avaliacaoTurmaModel);
                await AdicionarAvaliacaoAosAlunosDaTurma(avaliacaoTurmaModel.TurmaID, idAvaliacaoDaTurma);
                return Json(new { Ok = true, ResponseMessage = "Avaliação adicionada!"},JsonRequestBehavior.AllowGet);
            }
            catch (CrudAvaliacaoException ex)
            {
                return Json(new { Ok = false, ResponseMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public Guid SetarIdAvaliacaDaTurma(AvaliacaoTurma  avaliacaoTurma)
        {
           return avaliacaoTurma.IdAvaliacaoTurma = Guid.NewGuid();
        }
        public async Task AdicionarAvaliacaoAosAlunosDaTurma(int turmaId, Guid avaliacaoId)
        {
            var turma =  await _bussinesTurma.ObterPorIdAsync(turmaId);
            if (turma.Matriculas.Count > 0)
                await _bussinesAvaliacaoTurma.AdicionarAvaliacaoAosAlunosDaTurma(turma.Matriculas, avaliacaoId);
        }
        public async  Task<ActionResult> EditarAvaliacaoDaTurma(Guid IdAvaliacaoDaTurma)
        {
            var avaliacaoTurmaVM = Mapper.Map<AvaliacaoTurma, AvaliacaoTurmaVM>(_bussinesAvaliacaoTurma.ObterPorId(IdAvaliacaoDaTurma));
            avaliacaoTurmaVM.AvaliacoesDisponiveis = Mapper.Map<IList<Avaliacao>, IList<AvaliacaoVM>>(await _bussinesAvaliacao.ObterTodosAsync()).ToList();
            return View(avaliacaoTurmaVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarAvaliacaoDaTurma(AvaliacaoTurmaVM avaliacaoTurmaVM)
        {
            avaliacaoTurmaVM.AvaliacoesDisponiveis = Mapper.Map<IList<Avaliacao>, IList<AvaliacaoVM>>(await _bussinesAvaliacao.ObterTodosAsync()).ToList();
            try
            {
                var avaliacaoTurmaModel = Mapper.Map<AvaliacaoTurmaVM, AvaliacaoTurma>(avaliacaoTurmaVM);
                
                await _bussinesAvaliacaoTurma.AlterarAsync(avaliacaoTurmaModel);

                TempData["Mensagem"] = "Avaliação alterada com sucesso!";

                return RedirectToAction("AvaliacoesDaTurma", new { Id = avaliacaoTurmaModel.TurmaID, NomeTurma = avaliacaoTurmaVM.Turma.Nome});
            }catch(CrudAvaliacaoException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(avaliacaoTurmaVM);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoverAvaliacoesDaTurma(FormCollection formCollection)
        {
            var avaliacaoTurmaID = Guid.Parse(formCollection["IdAvaliacaoTurma"]);
            var nomeTurma = formCollection["NomeTurma"].ToString();
            var avaliacaoTurmaModel = _bussinesAvaliacaoTurma.ObterPorId(avaliacaoTurmaID);
            try
            {
                await _bussinesAvaliacaoTurma.DeletarAsync(avaliacaoTurmaModel);
                TempData["MensagemSucesso"] = "Avaliação removida com sucesso.";
                return RedirectToAction(nameof(AvaliacoesDaTurma), new { Id = avaliacaoTurmaModel.TurmaID, NomeTurma = nomeTurma });
            }
            catch(CrudAvaliacaoException ex)
            {
                TempData["Mensagem"] = ex.Message;
                return RedirectToAction(nameof(AvaliacoesDaTurma), new { Id = avaliacaoTurmaModel.TurmaID, NomeTurma = nomeTurma });
            }
            
        }


    }
}
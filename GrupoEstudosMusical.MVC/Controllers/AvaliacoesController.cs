﻿
using AutoMapper;
using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class AvaliacoesController : Controller
    {
        // GET: Avaliacao
        public AvaliacoesController(IBussinesAvaliacao bussinesAvaliacao
            , IBussinesAvaliacaoTurma bussinesAvaliacaoTurma)
        {
            this._bussinesAvaliacao = bussinesAvaliacao;
            this._bussinesAvaliacaoTurma = bussinesAvaliacaoTurma;
        }
        readonly IBussinesAvaliacao _bussinesAvaliacao;
        readonly IBussinesAvaliacaoTurma _bussinesAvaliacaoTurma;
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

        public ActionResult AvaliacoesDaTurma(int Id)
        {
            ViewBag.Model = new AvaliacaoTurmaVM() { TurmaID = Id };
            ViewBag.Id = Id;
            var avaliacoes = Mapper.Map<IList<AvaliacaoTurma>, IList<AvaliacaoTurmaVM>>(_bussinesAvaliacaoTurma.ObterPelaTurma(Id));

            return View(avaliacoes);
        }
    }
}
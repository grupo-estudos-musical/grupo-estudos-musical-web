
using AutoMapper;
using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Models;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.App_Start;
using GrupoEstudosMusical.MVC.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using static GrupoEstudosMusical.Models.Entities.Instrumento;

namespace GrupoEstudosMusical.MVC.Controllers
{
  
    [AuthorizeGem]
    public class InstrumentosController : Controller
    {
        readonly IBussinesFabricante _bussinesFabricante;
        readonly IBussinesInstrumento _bussinesInstrumento;
        readonly IBussinesInstrumentoDoAluno _bussinesInstrumentoDoAluno;
        readonly IBussinesInventario _bussinesInventario;
        readonly IBussinesAluno _bussinesAluno;
        public InstrumentosController(IBussinesFabricante bussinesFabricante, IBussinesInstrumento bussinesInstrumento
            , IBussinesAluno bussinesAluno, IBussinesInstrumentoDoAluno bussinesInstrumentoDoAluno, IBussinesInventario bussinesInventario)
        {
            _bussinesFabricante = bussinesFabricante;
            _bussinesInstrumento = bussinesInstrumento;
            _bussinesAluno = bussinesAluno;
            _bussinesInstrumentoDoAluno = bussinesInstrumentoDoAluno;
            _bussinesInventario = bussinesInventario;
        }
        // GET: Instrumento
        public async Task<ActionResult> Index()
        {
            var instrumentos = Mapper.Map<IList<Instrumento>, IList<InstrumentoVM>>(await _bussinesInstrumento.ObterTodosAsync());
            return View(instrumentos);
        }

        public async Task<ActionResult> InstrumentosDoAluno(int alunoId, string nomeAluno)
        {
            await InicializarViewBagDeFabricantes();
            await IncializarViewBagInstrumentos();
            ViewBag.AlunoID = alunoId;
            ViewBag.NomeAluno = nomeAluno;
            var instrumentos = Mapper.Map<IList<InstrumentoDoAluno>,IList<InstrumentoDoAlunoVM>>(_bussinesInstrumentoDoAluno.ObterInstrumentosDoAluno(alunoId));
            instrumentos.ForEach(i => i.NomeInstrumentoAluno = i.Inventario != null ? i.Inventario.Instrumento.Nome : "");
            return View(instrumentos);
        }
        public async Task<ActionResult> Novo()
        {
            await InicializarViewBagDeFabricantes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(InstrumentoVM instrumentoVM)
        {
            try
            {
                var instrumentoModel = Mapper.Map<InstrumentoVM, Instrumento>(instrumentoVM);
                await _bussinesInstrumento.InserirAsync(instrumentoModel);
                await AdicionarInstrumentoNoInventario(instrumentoModel);

                TempData["Mensagem"] = "Instrumento cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(instrumentoVM);
            }
            
            
        }

        public ActionResult Editar(Guid ID)
        {
            var instrumento = Mapper.Map<Instrumento,InstrumentoVM>(_bussinesInstrumento.ObterPorIdGuid(ID));

            return View(instrumento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(InstrumentoVM instrumento)
        {
            try
            {
                var instrumentoModel = Mapper.Map<InstrumentoVM, Instrumento>(instrumento);

                await _bussinesInstrumento.AlterarAsync(instrumentoModel);

                TempData["Mensagem"] = "Instrumento editado com sucesso";

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;

                return View(instrumento);
            }
        }
        async Task AdicionarInstrumentoNoInventario(Instrumento instrumento)
        {
            await _bussinesInventario.InserirAsync(new Inventario()
            {
                DataCadastro = DateTime.Now,
                EstoqueMinimo = 0,
                QuantidadeDisponivel = 0,
                InventarioID = instrumento.IntrumentoID
            });
        }

        async Task InicializarViewBagDeFabricantes()
        {
            ViewBag.Fabricantes = Mapper.Map<IList<Fabricante>, IList<FabricanteVM>>(await _bussinesFabricante.ObterTodosAsync());
        }
        async Task IncializarViewBagInstrumentos()
        {
            ViewBag.Instrumentos = Mapper.Map<IList<Instrumento>, IList<InstrumentoVM>>(await _bussinesInstrumento.ObterTodosDisponivelParaEmprestimo());
        }

        [HttpPost]
        public async Task<JsonResult> EfetuarEmprestimo(InstrumentoDoAlunoVM instrumentoDoAlunoVM)
        {
            try
            {
                instrumentoDoAlunoVM.UsuarioId = Convert.ToInt32(Session["idUsuario"].ToString());
                var instrumentoEmprestimoModel = Mapper.Map<InstrumentoDoAlunoVM, InstrumentoDoAluno>(instrumentoDoAlunoVM);
                await _bussinesInstrumentoDoAluno.InserirAsync(instrumentoEmprestimoModel);
                return Json(new { result = true, mensagem = "Empréstimo realizado com sucesso!" }, JsonRequestBehavior.AllowGet);
            }
            catch (CrudEmprestimoException ex)
            {
                return Json(new { result = false, mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
            
        }

        [HttpPost]
        public async Task<JsonResult> RealizarDevolucao(Guid instrumentoDoAlunoID)
        {
            try
            {
                await _bussinesInstrumentoDoAluno.RealizarDevolucao(instrumentoDoAlunoID);
                return Json(new { result = true, mensagem = "Devolução realizada com sucesso!" }, JsonRequestBehavior.AllowGet);
            }catch(Exception ex)
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Deletar(FormCollection formCollection)
        {
            Guid.TryParse(formCollection["id"].ToString(), out var id);
            var fabricanteModel = _bussinesInstrumento.ObterPorIdGuid(id);
            await _bussinesInstrumento.DeletarAsync(fabricanteModel);
            TempData["Mensagem"] = "Instrumento Apagado com Sucesso.";
            return RedirectToAction(nameof(Index));
        }



    }
}
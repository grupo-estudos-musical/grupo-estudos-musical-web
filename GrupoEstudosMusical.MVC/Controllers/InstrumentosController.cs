
using AutoMapper;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using static GrupoEstudosMusical.Models.Entities.Instrumento;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class InstrumentosController : Controller
    {
        readonly IBussinesFabricante _bussinesFabricante;
        readonly IBussinesInstrumento _bussinesInstrumento;
        readonly IBussinesInstrumentoDoAluno _bussinesInstrumentoDoAluno;
        readonly IBussinesAluno _bussinesAluno;
        public InstrumentosController(IBussinesFabricante bussinesFabricante, IBussinesInstrumento bussinesInstrumento
            , IBussinesAluno bussinesAluno, IBussinesInstrumentoDoAluno bussinesInstrumentoDoAluno)
        {
            _bussinesFabricante = bussinesFabricante;
            _bussinesInstrumento = bussinesInstrumento;
            _bussinesAluno = bussinesAluno;
            _bussinesInstrumentoDoAluno = bussinesInstrumentoDoAluno;
        }
        // GET: Instrumento
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> InstrumentosDoAluno(int alunoId)
        {
            await InicializarViewBagDeFabricantes();
            await IncializarViewBagInstrumentos();
            ViewBag.AlunoID = alunoId;
            var instrumentos = Mapper.Map<IList<InstrumentoDoAluno>,IList<InstrumentoDoAlunoVM>>(_bussinesInstrumentoDoAluno.ObterInstrumentosDoAluno(alunoId));
            return View(instrumentos);
        }
        public async Task<ActionResult> Novo()
        {
            await InicializarViewBagDeFabricantes();
            return View();
        }

        async Task InicializarViewBagDeFabricantes()
        {
            ViewBag.Fabricantes = Mapper.Map<IList<Fabricante>, IList<FabricanteVM>>(await _bussinesFabricante.ObterTodosAsync());
        }
        async Task IncializarViewBagInstrumentos()
        {
            ViewBag.Instrumentos = Mapper.Map<IList<Instrumento>, IList<InstrumentoVM>>(await _bussinesInstrumento.ObterTodosAsync());
        }

        [HttpPost]
        public async Task<JsonResult> EfetuarEmprestimo(InstrumentoDoAlunoVM instrumentoDoAlunoVM)
        {
            try
            {
                var instrumentoEmprestimoModel = Mapper.Map<InstrumentoDoAlunoVM, InstrumentoDoAluno>(instrumentoDoAlunoVM);
                await _bussinesInstrumentoDoAluno.InserirAsync(instrumentoEmprestimoModel);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
            
            
        }



    }
}
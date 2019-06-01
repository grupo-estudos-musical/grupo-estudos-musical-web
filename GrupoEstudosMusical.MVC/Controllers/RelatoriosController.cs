using GrupoEstudosMusical.Bussines;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Entities.Relatorios;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.App_Start;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    [AuthorizeGem]
    public class RelatoriosController : Controller
    {
        // GET: Relatorios
        public RelatoriosController(IBussinesOcorrencia bussinesOcorrencia, IBussinesMatricula bussinesMatricula
            , IBussinesTurma bussinesTurma)
        {
            this.bussinesOcorrencia = bussinesOcorrencia;
            this.bussinesMatricula = bussinesMatricula;
            this.bussinesTurma = bussinesTurma;
        }
        readonly IBussinesOcorrencia bussinesOcorrencia;
        readonly IBussinesMatricula bussinesMatricula;
        readonly IBussinesTurma bussinesTurma;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GerarRelatoriosDeOcorrenciasPorTurma(int turmaID)
        {
            ViewBag.TurmaID = turmaID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GerarRelatoriosDeOcorrenciasPorTurma(DateTime dataInicial, DateTime dataFinal, int turmaID)
        {
            try
            {
                var listaDeOcorrenciasPorTurma = bussinesOcorrencia.ObterOcorrenciasPorTurma(dataInicial, dataFinal, turmaID);
                var relatorioGerado = Relatorios.GerarRelatorio<OcorrenciasParaRelatorio>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/ocorrencias.frx"), listaDeOcorrenciasPorTurma, "Dados", TiposDeRelatorios.PDF, null);
                return File(relatorioGerado, "application/pdf", $"RelatorioDeOcorrenciasPorTurma{DateTime.Now}.pdf");
            }catch(Exception e)
            {
                TempData["_MensagemErro"] = e.Message;
                return View();
            }
            
        }



        public async  Task<ActionResult> GerarRelatorioOcorrenciasPorAluno(int AlunoId)
        {
            var listaOcorrenciasAluno = new List<OcorrenciasParaRelatorio>()
            {
                await bussinesOcorrencia.ObterOcorrenciasParaRelatorio(AlunoId)
            };

            var relatorioGerado = Relatorios.GerarRelatorio<OcorrenciasParaRelatorio>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/ocorrencias.frx"), listaOcorrenciasAluno, "Dados", TiposDeRelatorios.PDF, null);

            return File(relatorioGerado, "application/pdf", $"RelatorioOcorrencias{DateTime.Now}.pdf");
        }

        public async Task<ActionResult> GerarBoletim(int matriculaID)
        {
            var listaBoletim = new List<Boletim>()
            {
                await bussinesMatricula.ObterBoletimAluno(matriculaID)
            };

            var relatorioGerador = Relatorios.GerarRelatorio<Boletim>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/boletim.frx"), listaBoletim, "Dados", TiposDeRelatorios.PDF, null);
            return File(relatorioGerador, "application/pdf", "boletimAluno.pdf");
        }

        public async Task<ActionResult> GerarBoletimdaTurma(int ID)
        {
            var listaBoletimDaTurma = await bussinesMatricula.ObterBoletimDaTurma(ID);

            var relatorioGerador = Relatorios.GerarRelatorio<Boletim>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/boletim.frx"), listaBoletimDaTurma ,"Dados", TiposDeRelatorios.PDF, null);
            return File(relatorioGerador, "application/pdf", $"boletimDaTurma{DateTime.Now}.pdf");
        }


    }
}
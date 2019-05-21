using GrupoEstudosMusical.Bussines;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Entities.Relatorios;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class RelatoriosController : Controller
    {
        // GET: Relatorios
        public RelatoriosController(IBussinesOcorrencia bussinesOcorrencia, IBussinesMatricula bussinesMatricula)
        {
            this.bussinesOcorrencia = bussinesOcorrencia;
            this.bussinesMatricula = bussinesMatricula;
        }
        readonly IBussinesOcorrencia bussinesOcorrencia;
        readonly IBussinesMatricula bussinesMatricula;

        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult RelatorioOcorrencia(int AlunoId)
        {
            var listaOcorrenciasAluno = bussinesOcorrencia.ObterOcorrenciasPorAluno(AlunoId);

            var relatorioGerado = Relatorios.GerarRelatorio<Ocorrencia>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/ocorrencias.frx"), listaOcorrenciasAluno, "Dados", TiposDeRelatorios.PDF, null);

            return File(relatorioGerado, "application/pdf", "relatorioocorrencias.pdf");
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
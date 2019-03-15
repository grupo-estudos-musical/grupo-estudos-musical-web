using GrupoEstudosMusical.Bussines;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.Models.Interfaces.Bussines;

using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class RelatoriosController : Controller
    {
        // GET: Relatorios
        public RelatoriosController(IBussinesOcorrencia bussinesOcorrencia)
        {
            this.bussinesOcorrencia = bussinesOcorrencia;
        }
        readonly IBussinesOcorrencia bussinesOcorrencia;

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

       
    }
}
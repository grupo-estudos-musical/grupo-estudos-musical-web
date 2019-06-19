using GrupoEstudosMusical.Bussines;
using GrupoEstudosMusical.Models.Entities.Relatorios;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.MVC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class RelatorioController : Controller
    {
        // GET: Relatorio
        public RelatorioController(IBussinesOcorrencia bussinesOcorrencia)
        {
            _bussinesOcorrencia = bussinesOcorrencia;
        }
        public ActionResult Index()
        {
            return View();
        }
        private readonly IBussinesOcorrencia _bussinesOcorrencia;

        public async Task<ActionResult> GerarRelatorioOcorrenciasPorAluno(int AlunoId)
        {
            try
            {
                var listaOcorrenciasAluno = new List<OcorrenciasParaRelatorio>()
                {
                    await _bussinesOcorrencia.ObterOcorrenciasParaRelatorio(AlunoId)
                };
                listaOcorrenciasAluno.ForEach(l => l.ImagemAluno = AlunoHelper.ObterByteImagemAluno(l.Aluno.ImagemUrl, Server));
                var relatorioGerado = Relatorios.GerarRelatorio<OcorrenciasParaRelatorio>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/ocorrencias.frx"), listaOcorrenciasAluno, "Dados", TiposDeRelatorios.PDF, null);
               // EnviarEmailAoALunoComRelatorio(listaOcorrenciasAluno.FirstOrDefault().Aluno, new List<byte[]>() { relatorioGerado });
                return File(relatorioGerado, "application/pdf", $"RelatorioOcorrencias{DateTime.Now}.pdf");

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message + ex.StackTrace;
                return RedirectToAction("Index", "Home");
            }

        }

    }
}
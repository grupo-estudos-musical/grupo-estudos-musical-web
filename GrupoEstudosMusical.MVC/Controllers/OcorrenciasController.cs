using GrupoEstudosMusical.Models.Interfaces.Bussines;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using GrupoEstudosMusical.Models.Entities;

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
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Novo()
        {
            ViewBag.Turmas = await _bussinesTurma.ObterTodosAsync();
            ViewBag.Tipos = Enum.GetNames(typeof(TiposOcorrenciaEnum));
            return View();
        }

    }
}
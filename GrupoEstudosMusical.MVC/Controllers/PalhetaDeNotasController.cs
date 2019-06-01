
using GrupoEstudosMusical.MVC.App_Start;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    [AuthorizeGem]
    public class PalhetaDeNotasController : Controller
    {
        // GET: PalhetaDeNotas
        public ActionResult Index()
        {
            return View();
        }
    }
}
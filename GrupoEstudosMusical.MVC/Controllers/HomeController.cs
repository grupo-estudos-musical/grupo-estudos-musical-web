using GrupoEstudosMusical.MVC.App_Start;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    [AuthorizeGem]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
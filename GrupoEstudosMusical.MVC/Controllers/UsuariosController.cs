using GrupoEstudosMusical.MVC.Models;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class UsuariosController : Controller
    {
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logar(AutenticarVM autenticarVM)
        {
            return Redirect("/");
        }
    }
}
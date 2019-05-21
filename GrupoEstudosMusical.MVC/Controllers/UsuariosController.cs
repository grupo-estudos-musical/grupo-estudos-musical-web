using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}
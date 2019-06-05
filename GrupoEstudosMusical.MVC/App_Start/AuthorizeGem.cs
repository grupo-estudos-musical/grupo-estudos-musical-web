using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.App_Start
{
    public class AuthorizeGem : ActionFilterAttribute
    {
        public AuthorizeGem() { }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["idUsuario"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/Login", false);
            }
        }
    }
}
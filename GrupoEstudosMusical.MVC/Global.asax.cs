using GrupoEstudosMusical.Email.Services;
using GrupoEstudosMusical.Email.Services.Generic;
using GrupoEstudosMusical.IoC;
using GrupoEstudosMusical.MVC.AutoMapper;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GrupoEstudosMusical.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = DiContainer.RegisterDependencies();
            container.RegisterInstance<IEmailService>(new GmailService(Server.MapPath("")));

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            AutoMapperConfig.RegisterMappings();
        }
    }
}

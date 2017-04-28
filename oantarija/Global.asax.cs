using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using oantarija.Models;
namespace oantarija
{
    public class MvcApplication : System.Web.HttpApplication
    {
        oantarijaEntities BD = new oantarijaEntities();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            string roles = string.Empty;
            if (User.Identity.IsAuthenticated)
            {
                var usuario = BD.usuario.Single(o => o.correo == User.Identity.Name);
                switch (usuario.rol)
                {
                    case 1:
                        roles = "ADMINISTRADOR";
                        break;
                    case 2:
                        roles = "ESTANDAR";
                        break;
                }
                HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(User.Identity.Name, "Forms"), roles.Split(';'));
            }
        }
    }
}

using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Bootstrap;
using Bootstrap.AutoMapper;
using Bootstrap.Windsor;
using Castle.Windsor;

namespace WeathermanWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Use bootstrapper to setup AutoMapper, and Castle Windsor.
            //Automapper was not actually used in this project.
            Bootstrapper.With.AutoMapper().Windsor().Start();
        }

        protected void Application_End()
        {
// Check if the CastleWindsor container exists, and dispose of it if necessary.
            var container = (IWindsorContainer)Bootstrapper.Container;
            if (container != null)
            {
                container.Dispose();
            }
        }

    }
}

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
            Bootstrapper.With.AutoMapper().Windsor().Start();
            BootstrapContainer();
        }

        protected void Application_End()
        {
            //_container.Dispose();
            var container = (IWindsorContainer)Bootstrapper.Container;
            if (container != null)
            {
                container.Dispose();
            }
        }


        private static void BootstrapContainer()
        {
            //var container = (IWindsorContainer)Bootstrapper.Container;
            //var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            //ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            //container.Register(Component.For<ITopicService>()
            //         .ImplementedBy<TopicService>());
        }
    }
}

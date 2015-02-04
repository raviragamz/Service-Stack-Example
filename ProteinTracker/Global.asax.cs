using ProteinTracker.Api;
using ServiceStack;
using ServiceStack.Redis;
using ServiceStack.WebHost.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProteinTracker
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //Below is commented out bcoz: http://stackoverflow.com/questions/12361860/service-stack-on-mvc4
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            new ProteinTrackerAppHost().Init();//Starts up our service, this is used instead of WebApiConfig.Register(GlobalConfiguration.Configuration);
        }
    }

    /// <summary>
    /// All ServiceSTack apps require AppHost
    /// </summary>
    public class ProteinTrackerAppHost : AppHostBase
    {
        public ProteinTrackerAppHost(): base("Protein Tracker Webservices", typeof(HelloService).Assembly)
        {

        }

        public override void Configure(Funq.Container container)
        {
            SetConfig(new EndpointHostConfig { ServiceStackHandlerFactoryPath = "api" });

            #region Register Redis

            //Register Redis. We are going to add Redis to our IOC container(remember ServiceStack has it's own IOC container),
            //so that we can get it from inside our "Services".
            container.Register<IRedisClientsManager>(c => new PooledRedisClientManager());
            container.Register<IRepository>(c => new Repository(c.Resolve<IRedisClientsManager>()));
            //by creating the repository pattern, we create an abstraction. Say if we want to move away from Redis, it will be an easy transition.

            #endregion
        }
    }
}
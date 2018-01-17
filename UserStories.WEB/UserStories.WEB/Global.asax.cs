using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using UserStories.BLL.EF;
using UserStories.BLL.Interfaces;
using UserStories.BLL.Interfase;
using UserStories.BLL.Repositories;
using UserStories.BLL.Services;
using UserStories.DAL.Repositories;
using UserStories.WEB.Controllers;

namespace UserStories.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<IdentityUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<ApplicationContext>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<StoriesServise>().As<IStoriesSevises>();
            builder.RegisterType<ClientManager>().As<IClientManager>();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}

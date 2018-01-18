using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using UserStories.BLL.EF;
using UserStories.BLL.Entities;
using UserStories.BLL.Identity;
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
            builder.RegisterType<IUnitOfWork>().As<IdentityUnitOfWork>();
            builder.RegisterType<ApplicationContext>();
            builder.RegisterType<IUserService>().As<UserService>();
            builder.RegisterType<StoriesServise>().As<IStoriesSevises>();
            builder.RegisterType<IStoriesSevises>().As<StoriesServise>();
            builder.RegisterType<IClientManager>().As<ClientManager>();
            builder.RegisterType<IApplicationUserManager>().As<ApplicationUserManager>();
            builder.RegisterType<ApplicationUserManager>().As<IApplicationUserManager>();
            builder.RegisterType<ApplicationRoleManager>().As<IApplicationRoleManager>();
            builder.RegisterType<IApplicationRoleManager>().As<ApplicationRoleManager>();



            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}

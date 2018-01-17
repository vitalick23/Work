using System.Reflection;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using UserStories.BLL.EF;
using UserStories.BLL.Interfaces;
using UserStories.BLL.Services;

[assembly: OwinStartup(typeof(UserStories.WEB.App_Start.Startup))]
namespace UserStories.WEB.App_Start
{
public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService(new ApplicationContext());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.EF;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfaces;
using UserStories.BLL.Interfase;
using UserStories.BLL.Repositories;


namespace UserStories.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(ApplicationContext applicationContext,
            IApplicationRoleManager roleManager,
            IApplicationUserManager userManager,
            IClientManager clientManager,
            IStoriesManager storiesManager)
        {
            return new UserService(new IdentityUnitOfWork(applicationContext, roleManager, userManager,
                clientManager, storiesManager),roleManager,userManager);
        }


    }
}

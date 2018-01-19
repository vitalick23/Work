using System;
using UserStories.BLL.EF;
using UserStories.BLL.Interfase;

namespace UserStories.BLL.Interfaces
{
    public interface IServiceCreator
    {
        IUserService CreateUserService(ApplicationContext applicationContext, 
            //IApplicationRoleManager roleManager,
            IApplicationUserManager userManager,
            IClientManager clientManager,
            IStoriesManager storiesManager);
    }
}

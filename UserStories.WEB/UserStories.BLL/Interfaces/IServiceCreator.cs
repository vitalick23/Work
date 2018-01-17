using System;
using UserStories.BLL.EF;

namespace UserStories.BLL.Interfaces
{
    public interface IServiceCreator
    {
        IUserService CreateUserService(ApplicationContext applicationContext);
    }
}

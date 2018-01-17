using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.EF;
using UserStories.BLL.Interfaces;
using UserStories.BLL.Repositories;


namespace UserStories.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(ApplicationContext applicationContext)
        {
            return new UserService(new IdentityUnitOfWork(applicationContext));
        }


    }
}

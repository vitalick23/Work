using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfaces;

namespace UserStories.BLL.Tests.Class
{
    class ServiceManager : IUserService
    {
        public ClaimsIdentity Authenticate(ApplicationUser userDto)
        {
            throw new NotImplementedException();
        }

        public Task Create(ApplicationUser user, string password)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task SetInitialData(ApplicationUser adminDto, List<string> roles)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using UserStories.BLL.Entities;

namespace UserStories.BLL.Interfaces
{
    public interface IApplicationUserManager : IDisposable
    {
        ApplicationUser FindUser(string email, string password);

        ClaimsIdentity CreateIdentity(ApplicationUser user, string applicationType);

        ApplicationUser FindByEmail(string email);
        bool CreateUsers(ApplicationUser user, string password);
    }
}

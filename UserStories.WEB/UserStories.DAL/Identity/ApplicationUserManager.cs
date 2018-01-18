using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfaces;


namespace UserStories.BLL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>, IApplicationUserManager
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
            
        }

        public ClaimsIdentity CreateIdentity(ApplicationUser user, string applicationType)
        {
            ClaimsIdentity result = CreateIdentity(user, applicationType);
            return result;
        }

        public bool CreateUsers(ApplicationUser user, string password)
        {
            var result = CreateAsync(user, password);
            if (result.Status == TaskStatus.Created) return true;
            return false;
        }

        public ApplicationUser FindUser(string email, string password)
        {
            return FindUser(email, password);
        }

        ApplicationUser IApplicationUserManager.FindByEmail(string email)
        {
           return new ApplicationUser();
            
        }
    }
}

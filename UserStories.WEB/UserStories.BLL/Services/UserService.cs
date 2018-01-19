using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using UserStories.BLL.DTO;
using UserStories.BLL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfase;

namespace UserStories.BLL.Services
{
    public class UserService: IUserService
    {
       // private readonly IApplicationRoleManager _rolemanegr;
        IUnitOfWork Database { get; set; }
        public IApplicationUserManager _applicationUserManager;
        private IClientManager clientManager;
        private IStoriesManager storiesManager;

        public UserService(IUnitOfWork uow, 
      
            IApplicationUserManager applicationUserManager,
            IClientManager clientManager,IStoriesManager storiesManager)
        {
            //      IApplicationRoleManager rolemanegr,
            //     _rolemanegr = rolemanegr;
            _applicationUserManager = applicationUserManager;
            this.clientManager = clientManager;
            this.storiesManager = storiesManager;
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }



        public ClaimsIdentity Authenticate(ApplicationUser userDto)
        {
            if (userDto != null)
            {
                ClaimsIdentity claim = null;
                ApplicationUser user = _applicationUserManager.FindUser(userDto.Email, userDto.PasswordHash);
                if (user != null)
                {
                    claim = _applicationUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    return claim;
                }
            }

            return null;

        }
       

        public bool Create(string email,string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) return false;

            ApplicationUser user = _applicationUserManager.FindByEmail(email);
                if (user != null)
                {
                    user = new ApplicationUser {Email = email, UserName = email};
                    if (_applicationUserManager.CreateUsers(user, password))
                    {
                        clientManager.Create(new ClientProfile
                        {
                            Id = user.Id,
                            Adress = email,
                            Name = email
                        });
                        try
                        {
                            Database.SaveAsync();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            return false;
                        }
                    }
                }

            return false;
        }

        //no test
        public bool CreateStories(Stories item)
        {
            if (item != null)
            {
                storiesManager.Create(item);
            }

            return false;
        }

        public Task SetInitialData(ApplicationUser adminDto, List<string> roles)
        {
            throw new NotImplementedException();
        }
    }
    //public async Task SetInitialData(ApplicationUser adminDto, List<string> roles)
    //{
    //    foreach (string roleName in roles)
    //    {
    //        var role = await Database.RoleManager.FindByNameAsync(roleName);
    //        if (role == null)
    //        {
    //            role = new ApplicationRole { Name = roleName };
    //            await Database.RoleManager.CreateAsync(role);
    //        }
    //    }
    //    await Create(adminDto);
    //}  
}

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using UserStories.BLL.DTO;
using UserStories.BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UserStories.BLL.Entities;
using UserStories.BLL.Infrastructure;
using UserStories.BLL.Interfase;

namespace UserStories.BLL.Services
{
    public class UserService: IUserService
    {
        private readonly IApplicationRoleManager _rolemanegr;
        IUnitOfWork Database { get; set; }
        public IApplicationUserManager _applicationUserManager;
        private IClientManager clientManager;

        public UserService(IUnitOfWork uow, 
            IApplicationRoleManager rolemanegr,
            IApplicationUserManager applicationUserManager,
            IClientManager clientManager)
        {
            _rolemanegr = rolemanegr;
            _applicationUserManager = applicationUserManager;
            this.clientManager = clientManager;
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }



        public Task<ClaimsIdentity> Authenticate(ApplicationUser userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = _applicationUserManager.FindUser(userDto.Email, userDto.PasswordHash); //await Database.UserManager.FindAsync(userDto.Email, userDto.PasswordHash);
            if (user != null)
                claim = _applicationUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie); //await Database.UserManager.CreateIdentityAsync(user,
                    //DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
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

        public async Task Create(ApplicationUser applicationUser,string password)
        {

                ApplicationUser user = _applicationUserManager.FindByEmail(applicationUser.Email);
                if (user == null)
                {
                    user = new ApplicationUser {Email = user.Email, UserName = user.Email};
                    var result = _applicationUserManager.CreateUsers(user, password); //await Database.UserManager.CreateAsync(user, applicationUser.PasswordHash);
                    //await Database.UserManager.AddToRoleAsync(user.Id, applicationUser.Roles.);

                    ClientProfile clientProfile =
                        new ClientProfile {Id = user.Id, Adress = applicationUser.clientProfile.Adress, Name = applicationUser.clientProfile.Adress};
                    clientManager.Create(clientProfile);
                    await Database.SaveAsync();
                }
        }

        

        public Task SetInitialData(ApplicationUser adminDto, List<string> roles)
        {
            throw new NotImplementedException();
        }

       
    }
}

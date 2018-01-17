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
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }



        public async Task<ClaimsIdentity> Authenticate(ApplicationUser userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.PasswordHash);
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }
        public async Task SetInitialData(ApplicationUser adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public async Task<OperationDetails> Create(ApplicationUser applicationUser)
        {
            try
            {
                ApplicationUser user = await Database.UserManager.FindByEmailAsync(applicationUser.Email);
                if (user == null)
                {
                    user = new ApplicationUser {Email = user.Email, UserName = user.Email};
                    var result = await Database.UserManager.CreateAsync(user, applicationUser.PasswordHash);
                    if (result.Errors.Count() > 0)
                        return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                    //await Database.UserManager.AddToRoleAsync(user.Id, applicationUser.Roles.);

                    ClientProfile clientProfile =
                        new ClientProfile {Id = user.Id, Adress = applicationUser.clientProfile.Adress, Name = applicationUser.clientProfile.Adress};
                    Database.ClientManager.Create(clientProfile);
                    await Database.SaveAsync();
                    return new OperationDetails(true, "Регистрация успешно пройдена", "");
                }
                else
                {
                    return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
                }
            }
            catch (Exception ex)
            {
                return new OperationDetails(false,ex.Message);
            }
        }
    }
}

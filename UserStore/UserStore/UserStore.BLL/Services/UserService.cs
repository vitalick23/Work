using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using UserStore.BLL.Interfaces;
using System.Collections.Generic;
using UserStore.BLL.Entities;

namespace UserStore.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<bool> Create(ApplicationUser userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                await Database.UserManager.CreateAsync(user, userDto.PasswordHash);
                
                //await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                
                //ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                //Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return true;

            }
            else
            {
                return false;
            }
        }

        public async Task<ClaimsIdentity> Authenticate(ApplicationUser userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.PasswordHash);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if(user!=null)
                claim= await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
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

        public void Dispose()
        {
            Database.Dispose();
        }
    }

    
}

using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using UserStore.BLL.Entities;

namespace UserStore.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        UserManager<ApplicationUser> UserManager { get; }
        
        RoleManager<ApplicationRole> RoleManager { get; }

        Task SaveAsync();
    }
}

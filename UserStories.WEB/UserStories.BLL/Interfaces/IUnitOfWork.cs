using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.Identity;

namespace UserStories.BLL.Interfase
{
    public interface IUnitOfWork :IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IStoriesManager StoriesManager { get; }
        Task SaveAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using UserStories.BLL.Interfaces;

namespace UserStories.BLL.Interfase
{
    public interface IUnitOfWork :IDisposable
    {
        //IUserStore<UserManager<>>
        //ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        //IApplicationRoleManager RoleManager { get; }
        IApplicationUserManager UserManager { get; }
        IStoriesManager StoriesManager { get; }
        ICommentManager CommentManager { get; }
        Task SaveAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using UserStories.BLL.EF;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfaces;
using UserStories.BLL.Interfase;

namespace UserStories.BLL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        private IApplicationUserManager userManager;
       // private IApplicationRoleManager roleManager;
        private IClientManager clientManager;
        private IStoriesManager storiesManager;
        public ICommentManager commentManager;

        public IdentityUnitOfWork(ApplicationContext applicationContext,
      //      IApplicationRoleManager roleManager,
            IApplicationUserManager userManager,
            IClientManager clientManager,
            IStoriesManager storiesManager,
            ICommentManager commentManager)
        {
            db = applicationContext;
          //  this.roleManager = roleManager;
            this.userManager = userManager;
            this.clientManager = clientManager;
            this.storiesManager = storiesManager;
            this.commentManager = commentManager;
            // storiesManager = new StoriesManager(db);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public ICommentManager CommentManager
        {
            get { return commentManager; }
        }

       public IApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public IStoriesManager StoriesManager
        {
            get { return storiesManager; }
        }




        public void Dispose()
        {
            GC.SuppressFinalize(this);
            userManager.Dispose();
       //     roleManager.Dispose();
            clientManager.Dispose();
            //storiesManager.Dispose();
        }
    }
}

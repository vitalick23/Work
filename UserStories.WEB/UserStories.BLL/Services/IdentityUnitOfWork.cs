using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using UserStories.BLL.EF;
using UserStories.BLL.Entities;
using UserStories.BLL.Identity;
using UserStories.BLL.Interfase;


namespace UserStories.BLL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private ClientManager clientManager;
        private StoriesManager storiesManager;

        public IdentityUnitOfWork(ApplicationContext applicationContext)
        {
            db = applicationContext;
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            clientManager = new ClientManager(db);
            storiesManager = new StoriesManager(db);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public ApplicationUserManager UserManager
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

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            userManager.Dispose();
            roleManager.Dispose();
            clientManager.Dispose();
            storiesManager.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.DTO;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfaces;
using UserStories.BLL.Interfase;

namespace UserStories.BLL.Services
{
    public class StoriesServise : IStoriesSevises
    {
        IUnitOfWork Database { get; set; }
        IStoriesManager storiesManager;
        IApplicationUserManager applicationUserManager;

        public StoriesServise(IUnitOfWork uow,IStoriesManager storiesManager,IApplicationUserManager userManager)
        {
            this.storiesManager = storiesManager;
            this.applicationUserManager = userManager;
            Database = uow;

        }

        public void Dispose()
        {
            storiesManager.Dispose();
            applicationUserManager.Dispose();
            Database.Dispose();
        }

        public bool Create(Stories item)
        {
            if (item == null) return false;
            try
            {
                if (storiesManager.Create(item))
                {
                    Database.SaveAsync();
                    return true;
                }
                else return false;

            }
            
            catch (Exception)
            {
                return false;
            }
        }

        public List<Stories> GetStories()
        {
            return storiesManager.GetStories();
        }

        public List<Stories> GetStoriesByUserName(string userName)
        {
            var user = applicationUserManager.FindByEmail(userName);
            if (user == null) return null;
            return storiesManager.GetStoriesByUserName(userName);
        }

        public Stories GetStories(string idStory)
        {
            return storiesManager.GetStories(idStory);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.EF;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfase;


namespace UserStories.DAL.Repositories
{
    public class StoriesManager : IStoriesManager
    {
        public ApplicationContext Database { get; set; }
        public StoriesManager(ApplicationContext db)
        {
            Database = db;
        }

        public bool Create(Stories item)
        {
            try
            {
                Database.Stories.Add(item);
                return true;
            }
            catch (Exception) { return false; }
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public List<Stories> GetStories()
        {
            return Database.Stories.ToList();
        }

        public List<Stories> GetStoriesByUserName(string userName)
        {
           return Database.Stories.Where(x => x.IdUser == Database.Users.Where(q => q.UserName == userName).First().Id).ToList();
        }

        public Stories GetStories(string idStory)
        {
            return Database.Stories.Find(idStory);
        }
    }
}

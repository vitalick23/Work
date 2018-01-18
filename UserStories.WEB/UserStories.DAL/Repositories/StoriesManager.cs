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

        public void Create(Stories item)
        {
            Database.Stories.Add(item);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}



using System.Data.Entity;
using UserStories.BLL.EF;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfase;

namespace UserStories.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}

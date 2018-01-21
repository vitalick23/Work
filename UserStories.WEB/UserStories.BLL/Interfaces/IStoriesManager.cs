using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.Entities;

namespace UserStories.BLL.Interfase
{
    public interface IStoriesManager : IDisposable
    {
        bool Create(Stories item);

        List<Stories> GetStories();

        List<Stories> GetStoriesByUserName(string userName);

        Stories GetStories(string idStory);
    }
}

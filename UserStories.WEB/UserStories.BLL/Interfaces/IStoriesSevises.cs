using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.DTO;
using UserStories.BLL.Entities;

namespace UserStories.BLL.Interfaces
{
    public interface IStoriesSevises : IDisposable
    {
        Task<bool> Create(Stories stories);
    }
}

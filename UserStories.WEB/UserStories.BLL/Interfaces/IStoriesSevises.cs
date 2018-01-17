using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.DTO;
using UserStories.BLL.Entities;
using UserStories.BLL.Infrastructure;

namespace UserStories.BLL.Interfaces
{
    public interface IStoriesSevises : IDisposable
    {
        Task<OperationDetails> Create(Stories stories);
    }
}

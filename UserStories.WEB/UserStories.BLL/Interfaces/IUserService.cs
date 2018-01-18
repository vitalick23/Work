using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.DTO;
using UserStories.BLL.Entities;

namespace UserStories.BLL.Interfaces
{
    public interface IUserService :IDisposable
    {
        Task Create(ApplicationUser user, string password);
        ClaimsIdentity Authenticate(ApplicationUser userDto);
        Task SetInitialData(ApplicationUser adminDto, List<string> roles);
    }
}

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
        bool Create(string email, string password);
        ClaimsIdentity Authenticate(ApplicationUser userDto);
      
    }
}

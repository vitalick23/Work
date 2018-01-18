﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.Entities;

namespace UserStories.BLL.Interfaces
{
    public interface IApplicationRoleManager : IDisposable
    {
        bool CreateRole(ApplicationRole item);
      //  ApplicationRole FindByName(string roleName);


    }
}
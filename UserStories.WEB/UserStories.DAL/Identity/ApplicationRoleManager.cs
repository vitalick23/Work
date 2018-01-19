using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfaces;

namespace UserStories.BLL.Identity
{
    //public class ApplicationRoleManager : RoleManager<ApplicationRole>, IApplicationRoleManager
    //{
    //    public ApplicationRoleManager(RoleStore<ApplicationRole> store) : base(store)
    //    {

    //    }

    //    bool IApplicationRoleManager.CreateRole(ApplicationRole item)
    //    {
    //        var result = CreateAsync(item);
    //        if (result.Status == TaskStatus.Created) return true;
    //        return false;
    //    }

    //    //ApplicationRole IApplicationRoleManager.FindByName(string roleName)
    //    //{
    //    //    var result = FindByNameAsync(roleName);
    //    //    return result;
    //    //}
    //}
}

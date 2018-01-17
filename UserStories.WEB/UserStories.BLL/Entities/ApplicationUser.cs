using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserStories.BLL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile clientProfile { get; set; }
    }
}

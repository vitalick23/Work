using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using UserStories.BLL.Entities;


namespace UserStories.BLL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("ApplicationContext") { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Stories> Stories { get; set; }
        public DbSet<Comment> Comment { get; set; }
    }
}

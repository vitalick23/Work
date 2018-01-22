using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using UserStore.BLL.Entities;

namespace UserStore.DAL.EF
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string conectionString) : base(conectionString) { }
        
    }
}

using Microsoft.AspNet.Identity.EntityFramework;

namespace UserStore.BLL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}

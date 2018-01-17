using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStories.BLL.Entities
{
    public class Stories
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("ApplicationUser")]
        [Column(Order = 0)]
        public string IdUser { get; set; }
        public string Story { get; set; }
        public DateTime TimePublicate { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStories.BLL.Entities
{
    public class Comment
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("ApplicationUser")]
        [Column(Order = 0)]
        public string IdUser { get; set; }

        [ForeignKey("Stories")]
        [Column(Order = 0)]
        public string IdStories { get; set; }

        public string Text { get; set; }

        public DateTime TimePublicate { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Stories Stories { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserStories.WEB.Models
{
    public class StoriesModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string IdUser { get; set; }
        [Required]
        public string Story { get; set; }
        [Required]
        public DateTime TimePublicate { get; set; }
       // public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
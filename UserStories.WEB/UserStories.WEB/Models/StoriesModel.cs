using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UserStories.BLL.Entities;

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
        public static explicit operator Stories(StoriesModel model)
        {
            var stories = new Stories();
            stories.IdUser = stories.IdUser;
            stories.Story = stories.Story;
            stories.TimePublicate = model.TimePublicate;
            return stories;
        }
    }
}
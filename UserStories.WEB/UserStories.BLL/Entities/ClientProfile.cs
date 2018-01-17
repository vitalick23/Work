﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStories.BLL.Entities
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public  string Id { get; set; }

        public string Name { get; set; }
        public string Adress { get; set; }
        
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}

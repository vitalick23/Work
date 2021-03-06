﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.Entities;

namespace UserStories.BLL.Interfaces
{
    interface ICommentService : IDisposable
    {
        bool CreateComment(Comment item);

        List<Comment> GetCommentByIdStory(string IdStory);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfaces;
using UserStories.BLL.Interfase;

namespace UserStories.BLL.Services
{
    public class CommentService : ICommentService
    {
        IUnitOfWork Database { get; set; }
        ICommentManager commentManager;

        public CommentService(IUnitOfWork unitOfWork,ICommentManager commentManager)
        {
            Database = unitOfWork;
            this.commentManager = commentManager;
        }

        public bool CreateComment(Comment item)
        {
            return commentManager.CreateComment(item);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public List<Comment> GetCommentByIdStory(string IdStory)
        {
            return commentManager.GetCommentByIdStory(IdStory);
        }
    }
}

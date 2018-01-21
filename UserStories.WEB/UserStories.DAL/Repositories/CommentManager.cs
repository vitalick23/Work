using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.EF;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfaces;

namespace UserStories.DAL.Repositories
{
    public class CommentManager : ICommentManager
    {
        public ApplicationContext Database { get; set; }

        public CommentManager(ApplicationContext db)
        {
            Database = db;
        }

        public bool CreateComment(Comment item)
        {
            try { Database.Comment.Add(item); }
            catch(Exception) { return false; }
            return true;
        }

        public List<Comment> GetCommentByIdStory(string IdStory)
        {
            return Database.Comment.ToList();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}

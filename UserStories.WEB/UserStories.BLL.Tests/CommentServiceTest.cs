using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfaces;
using UserStories.BLL.Services;
using static UserStories.BLL.Tests.StoriesServiseTests;

namespace UserStories.BLL.Tests
{
    [TestClass]
    public class CommentServiceTest
    {
        public FakeCommentManager fakeCommentManager;
        public FakeIUnitOfWork fakeUnitWork;
        
        public CommentServiceTest()
        {
            fakeCommentManager = new FakeCommentManager();
            fakeUnitWork = new FakeIUnitOfWork();
        }

        [TestMethod]
        public void CreateCommentifSuccess()
        {
            fakeCommentManager.SetFlagCreate(true);
           
            var commentService = new CommentService(fakeUnitWork,fakeCommentManager);

            var result = commentService.CreateComment(new Comment());
          
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateCommentifError()
        {
            fakeCommentManager.SetFlagCreate(false);

            var commentService = new CommentService(fakeUnitWork, fakeCommentManager);

            var result = commentService.CreateComment(new Comment());

            Assert.IsTrue(!result);
        }
        [TestMethod]
        public void GetCommentByIdStoryifSuccess()
        {
            fakeCommentManager.SetFlagCreate(false);

            var commentService = new CommentService(fakeUnitWork, fakeCommentManager);

            var result = commentService.GetCommentByIdStory("1");

            Assert.IsTrue(result != null);
        }
    }

    public class FakeCommentManager : ICommentManager
    {
        bool flagCreate = false;

        public void SetFlagCreate(bool flag)
        {
            flagCreate = flag;
        }

        public bool CreateComment(Comment item)
        {
            return flagCreate;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetCommentByIdStory(string IdStory)
        {
            return new List<Comment> { new Comment() };
        }
    }
}


using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfaces;
using UserStories.BLL.Interfase;
using UserStories.BLL.Services;

namespace UserStories.BLL.Tests
{
    [TestClass]
    public class StoriesServiseTests
    {
        public IStoriesSevises storiesServises;
        public FakeIUnitOfWork faceUnitWork;
        public FaceUserManage faceUserManage;
        public FaceStoriesManage faceStoriesManage;

        public StoriesServiseTests()
        {
            faceUnitWork = new FakeIUnitOfWork();
            faceUserManage = new FaceUserManage();
            faceStoriesManage = new FaceStoriesManage();
        }
        #region CreateTest
        [TestMethod]
        public void CreateifSuccess()
        {
            //ARANGE
            var story = new Stories();
            faceStoriesManage.SetFlagCreate(true);
            faceUnitWork.SetFlagSave(true);
            storiesServises = new StoriesServise(faceUnitWork, faceStoriesManage, faceUserManage);
            //ACT
            var result = storiesServises.Create(story);
            //ASSERT
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateifStoriesNUll()
        {
            //ARANGE
            
            faceStoriesManage.SetFlagCreate(true);
            faceUnitWork.SetFlagSave(true);
            storiesServises = new StoriesServise(faceUnitWork, faceStoriesManage, faceUserManage);
            //ACT
            var result = storiesServises.Create(null);
            //ASSERT
            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void CreateifStoriesNotCreate()
        {
            var story = new Stories();
            faceStoriesManage.SetFlagCreate(false);
            faceUnitWork.SetFlagSave(true);
            storiesServises = new StoriesServise(faceUnitWork, faceStoriesManage, faceUserManage);

            var result = storiesServises.Create(story);
          
            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void CreateifStoriesNotSave()
        {
            var story = new Stories();
            faceStoriesManage.SetFlagCreate(true);
            faceUnitWork.SetFlagSave(false);
            storiesServises = new StoriesServise(faceUnitWork, faceStoriesManage, faceUserManage);

            var result = storiesServises.Create(story);

            Assert.IsTrue(!result);
        }
        #endregion

        #region GetStories
        [TestMethod]
        public void GetStoriesALL()
        {
            faceStoriesManage.SetFlagReturnStories(true);
            storiesServises = new StoriesServise(faceUnitWork, faceStoriesManage, faceUserManage);

            var result = storiesServises.GetStories();

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetStoriesALLifReturnNull()
        {
            faceStoriesManage.SetFlagReturnStories(false);
            storiesServises = new StoriesServise(faceUnitWork, faceStoriesManage, faceUserManage);

            var result = storiesServises.GetStories();

            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void GetStoriesById()
        {
            Stories stories = new Stories { Id = "1" };
            faceStoriesManage.SetStories(stories);
            storiesServises = new StoriesServise(faceUnitWork, faceStoriesManage, faceUserManage);

            var result = storiesServises.GetStories("1");

            Assert.IsTrue(result.Id == stories.Id);
        }

        [TestMethod]
        public void GetStoriesByIdNotFound()
        {
            Stories stories = new Stories { Id = "2" };
            faceStoriesManage.SetStories(stories);
            storiesServises = new StoriesServise(faceUnitWork, faceStoriesManage, faceUserManage);

            var result = storiesServises.GetStories("1");

            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void GetStoriesByUserNameIfSuccess()
        {
            ApplicationUser user = new ApplicationUser { Id = "1", UserName = "aladin" };
            Stories stories = new Stories { IdUser = "1" };
            faceStoriesManage.SetStories(stories);
            faceUserManage.SetApplicattionUser(user);
            storiesServises = new StoriesServise(faceUnitWork, faceStoriesManage, faceUserManage);

            var result = storiesServises.GetStoriesByUserName("aladin");

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetStoriesByUserNameIfNotFoundUser()
        {
            ApplicationUser user = new ApplicationUser { Id = "1", UserName = "ala" };
            Stories stories = new Stories { IdUser = "1" };
            faceStoriesManage.SetStories(stories);
            faceUserManage.SetApplicattionUser(user);
            storiesServises = new StoriesServise(faceUnitWork, faceStoriesManage, faceUserManage);

            var result = storiesServises.GetStoriesByUserName("aladin");

            Assert.IsTrue(result == null);
        }

        #endregion

        public class FakeIUnitOfWork : IUnitOfWork
        {
            private bool flagSave = false;
            public IClientManager ClientManager => throw new System.NotImplementedException();

            public IApplicationUserManager UserManager => throw new System.NotImplementedException();

            public IStoriesManager StoriesManager => throw new System.NotImplementedException();

            public void SetFlagSave(bool flag)
            {
                flagSave = flag;
            }
            public void Dispose()
            {
                throw new System.NotImplementedException();
            }

            public Task SaveAsync()
            {
                if (!flagSave) return Task.FromCanceled(System.Threading.CancellationToken.None);
                return Task.CompletedTask;
            }
        }

        public class FaceUserManage : IApplicationUserManager
        {
            ApplicationUser user;

            public void SetApplicattionUser(ApplicationUser item)
            {
                user = item;
            }

            public ClaimsIdentity CreateIdentity(ApplicationUser user, string applicationType)
            {
                throw new System.NotImplementedException();
            }

            public bool CreateUsers(ApplicationUser user, string password)
            {
                throw new System.NotImplementedException();
            }

            public void Dispose()
            {
                throw new System.NotImplementedException();
            }

            public ApplicationUser FindByEmail(string userName)
            {
                if (user.UserName == userName) return user;
                else return null;
            }

            public ApplicationUser FindUser(string email, string password)
            {
                throw new System.NotImplementedException();
            }
        }
        public class FaceStoriesManage : IStoriesManager
        {
            bool flagCreate = false;
            bool flagReturnStories = false;
            Stories stories;
           
            public void SetStories(Stories item)
            {
                stories = item;
            }

            public void SetFlagCreate(bool flag)
            {
                flagCreate = flag;
            }

            public void SetFlagReturnStories(bool flag)
            {
                flagReturnStories = flag;
            }
            public bool Create(Stories item)
            {
                return flagCreate;
            }

            public void Dispose()
            {
                throw new System.NotImplementedException();
            }

            public List<Stories> GetStories()
            {
                if (flagReturnStories) return new List<Stories> { new Stories(), new Stories()};
                return null;
            }

            public Stories GetStories(string idStory)
            {
                if (stories.Id == idStory) return stories;
                return null;
            }

            public List<Stories> GetStoriesByUserName(string userName)
            {
                return (new List<Stories> { new Stories() });
            }
        }

    }
}

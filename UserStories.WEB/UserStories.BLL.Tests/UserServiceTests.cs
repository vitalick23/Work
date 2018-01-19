using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfaces;
using UserStories.BLL.Interfase;
using UserStories.BLL.Services;

namespace UserStories.BLL.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private FakeIApplicationUserManager fakeService;
        private FakeIClientManager fakeClientManager;

        public FakeIUnitOfWork faceUnitWork;

        public UserService userService;

        public ApplicationUser user { get; private set; }
        public UserServiceTests()
        {
            fakeService = new FakeIApplicationUserManager();
            fakeClientManager = new FakeIClientManager();
            faceUnitWork = new FakeIUnitOfWork();
            userService = new UserService(faceUnitWork, fakeService, fakeClientManager,null);
            user = new ApplicationUser { Email = "asdds@mail.ru" };
        }
        [TestMethod]
        public void AuthenticateifUserNull()
        {
            //ARANGE
            fakeService.SetUser(user);
            fakeService.SetClaims(null);
            var userService = new UserService(null, fakeService, null,null);
            //ACT
            var result = userService.Authenticate(null);
            //ASSERT
            Assert.IsNull(result);
        }

        [TestMethod]
        public void AuthenticateifUserNotNull()
        {
            //ARANGE
            var clain = new ClaimsIdentity();
            fakeService.SetClaims(clain);
            fakeService.SetUser(user);
            fakeService.CreateIdentity(user, null);
            var userService = new UserService(null,  fakeService, null,null);
            //ACT
            var result = userService.Authenticate(user);
            //ASSERT
            Assert.IsNotNull(result);;
        }

        [TestMethod]
        public void AuthenticateifUClaimNull()
        {
            //ARANGE
            fakeService.CreateIdentity(user, null);
            var userService = new UserService(null, fakeService, null,null);
            //ACT
            var result = userService.Authenticate(user);
            //ASSERT
            Assert.IsNull(result); ;
        }

        [TestMethod]
        public void AuthenticateifUserNotFind()
        {
            //ARANGE
            fakeService.SetUser(user);
            fakeService.CreateIdentity(user, null);
            var userService = new UserService(null, fakeService, null,null);
            //ACT
            var result = userService.Authenticate(user);
            //ASSERT
            Assert.IsNull(result); ;
        }

        [TestMethod]
        public void CreateifUserNotCreate()
        {
            //ARANGE
            string password = "**";
            fakeService.SetUser(user);
            fakeService.CreateUsers(user,password);
            var userService = new UserService(null, fakeService, null,null);
            //ACT
            var flag = userService.Create(user.Email, password);
            //ASSERT
            Assert.IsFalse(fakeService.GetCountUser() >= 1 && flag);
            //Assert.AreEqual(0,fakeService.GetCountUser());
        }

        [TestMethod]
        public void CreateifUserCreate()
        {
            //ARANGE
            var password = "S03an92!";
            fakeService.SetUser(user);
            fakeService.SetFlagCreate(true);
            fakeService.CreateUsers(user,password );
            faceUnitWork.SetFlagSave(true);
            var userService = new UserService(faceUnitWork, fakeService,fakeClientManager,null);
            //ACT
            var flag = userService.Create(user.Email, password);
            //ASSERT
            Assert.IsFalse(!flag);
        }

        [TestMethod]
        public void CreateifUserNotSave()
        {
            //ARANGE
            string password = "**";
            fakeService.SetUser(user);
            fakeService.SetFlagCreate(true);
            fakeService.CreateUsers(user, password);
            faceUnitWork.SetFlagSave(false);

            //ACT
            var flag = userService.Create(user.Email, password);
            //ASSERT
            Assert.AreEqual(false,flag);
        }

        [TestMethod]
        public void CreateifEmailEmpty()
        {
            //ARANGE
            string password = "**";
            fakeService.SetUser(user);
            fakeService.SetFlagCreate(true);
            fakeService.CreateUsers(user, password);
            faceUnitWork.SetFlagSave(false);

            //ACT
            var flag = userService.Create(user.Email, password);
            //ASSERT
            Assert.AreEqual(false, flag);
        }

        public class FakeIUnitOfWork : IUnitOfWork
        {
            private bool flagSave = false;
            public IClientManager ClientManager => throw new NotImplementedException();

            //public IApplicationRoleManager RoleManager => throw new NotImplementedException();

            public IApplicationUserManager UserManager => throw new NotImplementedException();

            public IStoriesManager StoriesManager => throw new NotImplementedException();

            public void Dispose()
            {
                throw new NotImplementedException();
            }
           
            public void SetFlagSave(bool flag)
            {
                flagSave = flag;
            }
            public Task SaveAsync()
            {
                if(!flagSave) return Task.FromCanceled(CancellationToken.None);
                return Task.CompletedTask;
            }
        }

        public class FakeIClientManager : IClientManager
        {
            public void Create(ClientProfile item)
            {
                
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }

        public class FakeIApplicationUserManager : IApplicationUserManager
        {
            public List<ApplicationUser> listUser;
           
            private bool flagCreate = false;
            private ApplicationUser user;
            private ClaimsIdentity claim;

            public FakeIApplicationUserManager()
            {
                listUser = new List<ApplicationUser>();
            }

            public int GetCountUser()
            {
                return listUser.Count;
            }
            public void addUser(ApplicationUser user)
            {
                listUser.Add(user);
            }
            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public void SetUser(ApplicationUser user)
            {
                this.user = user;
            }

            public void SetClaims(ClaimsIdentity claim)
            {
                this.claim = claim;
            }

            public ApplicationUser FindUser(string email, string password)
            {
                return user;
            }

            public ClaimsIdentity CreateIdentity(ApplicationUser user, string applicationType)
            {
                return claim;
            }

            public ApplicationUser FindByEmail(string email)
            {
                return user;
            }

            public void SetFlagCreate(bool flag)
            {
                flagCreate = flag;
            }
           
            public bool CreateUsers(ApplicationUser user, string password)
            {
                if(flagCreate) addUser(user);
                return flagCreate;
            }
        }

   
}
}


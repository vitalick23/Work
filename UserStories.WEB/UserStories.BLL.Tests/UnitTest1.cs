using System;
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
    public class UserServiceTests
    {
        [TestMethod]
        public void AuthenticateifUserNull()
        {
            //ARANGE
            var fakeService = new FakeIApplicationRoleManager();
            var user = new ApplicationUser();
            fakeService.SetUser(user);
            fakeService.SetClaims(null);
            var userService = new UserService(null, fakeService, null);
            //ACT
            var result = userService.Authenticate(null);
            //ASSERT
            Assert.IsNull(result);
        }

        [TestMethod]
        public void AuthenticateifUserNotNull()
        {
            //ARANGE
            var fakeService = new FakeIApplicationRoleManager();
            var clain = new ClaimsIdentity();
            var user = new ApplicationUser();
            fakeService.SetClaims(clain);
            fakeService.SetUser(user);
            fakeService.CreateIdentity(user, null);
            var userService = new UserService(null,  fakeService, null);
            //ACT
            var result = userService.Authenticate(user);
            //ASSERT
            Assert.IsNotNull(result);;
        }

        [TestMethod]
        public void AuthenticateifUClaimNull()
        {
            //ARANGE
            var fakeService = new FakeIApplicationRoleManager();
            var clain = new ClaimsIdentity();
            var user = new ApplicationUser();
            //fakeService.SetUser(user);
            fakeService.CreateIdentity(user, null);
            var userService = new UserService(null, fakeService, null);
            //ACT
            var result = userService.Authenticate(user);
            //ASSERT
            Assert.IsNull(result); ;
        }

        [TestMethod]
        public void AuthenticateifUserNotFind()
        {
            //ARANGE
            var fakeService = new FakeIApplicationRoleManager();
            var user = new ApplicationUser();
            fakeService.SetUser(user);
            fakeService.CreateIdentity(user, null);
            var userService = new UserService(null, fakeService, null);
            //ACT
            var result = userService.Authenticate(user);
            //ASSERT
            Assert.IsNull(result); ;
        }

        [TestMethod]
        public void CreateifUserNotCreate()
        {
            //ARANGE
            var fakeService = new FakeIApplicationRoleManager();
            string password = "**";
            var user = new ApplicationUser{Email = "sdf@mail.ru"};
            fakeService.SetUser(user);
            fakeService.CreateUsers(user,password);
            var userService = new UserService(null, fakeService, null);
            //ACT
            var flag = userService.Create(user.Email, password);
            //ASSERT
            Assert.IsFalse(flag || fakeService.GetCountUser() == 0);
            //Assert.AreEqual(0,fakeService.GetCountUser());
        }

        [TestMethod]
        public void CreateifUserCreate()
        {
            //ARANGE
            var fakeClientManager = new FakeIClientManager();
            var fakeService = new FakeIApplicationRoleManager();
            var faceUnitWork = new FakeIUnitOfWork();
            var user = new ApplicationUser{Email = "asdds@mail.ru"};
            var password = "S03an92!";
            fakeService.SetUser(user);
            fakeService.SetFlagCreate(true);
            fakeService.CreateUsers(user,password );
            var userService = new UserService(faceUnitWork, fakeService,fakeClientManager);
            //ACT
            var flag = userService.Create(user.Email, password);
            //ASSERT
            Assert.IsFalse(!flag);
        }

        [TestMethod]
        public void CreateifUserNotSave()
        {
            //ARANGE
            var fakeService = new FakeIApplicationRoleManager();
            var user = new ApplicationUser();
            string password = "**";
            fakeService.SetUser(user);
            fakeService.SetFlagCreate(true);
            fakeService.CreateUsers(user, password);
            var userService = new UserService(null, fakeService,null);
            //ACT
            userService.Create(user.Email, password);
            //ASSERT
            Assert.AreEqual(false, fakeService.GetFlagSave());
        }

        public class FakeIUnitOfWork : IUnitOfWork
        {
            public IClientManager ClientManager => throw new NotImplementedException();

            //public IApplicationRoleManager RoleManager => throw new NotImplementedException();

            public IApplicationUserManager UserManager => throw new NotImplementedException();

            public IStoriesManager StoriesManager => throw new NotImplementedException();

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public Task SaveAsync()
            {
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

        public class FakeIApplicationRoleManager : IApplicationUserManager
        {
            public List<ApplicationUser> listUser;
            private bool flagSave = false;
            private bool flagCreate = false;
            private ApplicationUser user;
            private ClaimsIdentity claim;

            public FakeIApplicationRoleManager()
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

            public bool GetFlagSave()
            {
                return flagSave;
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
            public void SetFlagSave(bool flag)
            {
                flagSave = flag;
            }
            public bool CreateUsers(ApplicationUser user, string password)
            {
                if(flagCreate) addUser(user);
                return flagCreate;
            }
        }

   
}
}


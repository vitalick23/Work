using System;
using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfaces;
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
            var userService = new UserService(null, null, fakeService, null);
            //ACT
            var result = userService.Authenticate(null);
            //ASERS
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
            var userService = new UserService(null, null, fakeService, null);
            //ACT
            var result = userService.Authenticate(user);
            //ASERS
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
            var userService = new UserService(null, null, fakeService, null);
            //ACT
            var result = userService.Authenticate(user);
            //ASERS
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
            var userService = new UserService(null, null, fakeService, null);
            //ACT
            var result = userService.Authenticate(user);
            //ASERS
            Assert.IsNull(result); ;
        }

        [TestMethod]
        public void CreateifUserNull()
        {
            //ARANGE
            var fakeService = new FakeIApplicationRoleManager();
            fakeService.SetUser(null);
           var userService = new UserService(null, null, fakeService, null);
            //ACT
            var result = userService.Create(null,null);
            //ASERS
            Assert.IsNull(result); ;
        }
        public class FakeIApplicationRoleManager : IApplicationUserManager
        {
            private bool flagSave = false;
            private bool flagCreate = false;
            private ApplicationUser user;
            private ClaimsIdentity claim;
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

            public bool CreateUsers(ApplicationUser email, string password)
            {
                flagSave = true;
                return flagCreate;
            }
        }

   
}
}


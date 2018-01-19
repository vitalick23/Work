using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfaces;
using UserStories.WEB.Controllers;
using System.Web.Mvc;
using UserStories.WEB.Models;

namespace UserStories.WEB.Test
{
    [TestClass]
    public class UserStoriesAccountControllerTest
    {
        public FaceUserService faceUserService { get; private set; }
        public AccountController controller { get; private set; }
        

        [TestInitialize]
        public void SetupContext()
        {
            faceUserService = new FaceUserService();
            controller = new AccountController(faceUserService);
           
        }

        [TestMethod]
        public void TestRegisterViewResultNotNull()
        {
            ViewResult result = controller.Register() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestRegisterPostModelError()
        {
            RegisterModel model = new RegisterModel();
            controller.ModelState.AddModelError("Name","Error");

            var result = controller.Register(model) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(model, result.Model);
        }

        [TestMethod]
        public void TestRegisterPostModelSuccess()
        {
            RegisterModel model = new RegisterModel();
            faceUserService.SetFlagCreate(true);

            var result = controller.Register(model) as RedirectToRouteResult;

            Assert.AreEqual("Index",result.RouteValues["action"]);
        }

        [TestMethod]
        public void TestRegisterPostCreateError()
        {
            RegisterModel model = new RegisterModel();

            var result = controller.Register(model) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(model, result.Model);
        }

        [TestMethod]
        public void TestLoginViewResultNotNull()
        {
            ViewResult result = controller.Login() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestLoginPostModelError()
        {
           LoginModel model = new LoginModel();
           controller.ModelState.AddModelError("Email", "Error");

            var result = controller.Login(model) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(model, result.Model);
        }

        [TestMethod]
        public void TestLoginPostSuccess()
        {
            LoginModel model = new LoginModel();
            faceUserService.SetFlagCreate(true);
            faceUserService.SetFlagCreateIdentity(true);
            
            var result = controller.Login(model) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        public class FaceUserService : IUserService
        {
            private bool flagCreate = false;
            private  bool flagCreateIdentity = false;

            public ClaimsIdentity Authenticate(ApplicationUser userDto)
            {
                if(flagCreateIdentity) return new ClaimsIdentity();
                return null;

            }

            public void SetFlagCreateIdentity(bool flag)
            {
                flagCreateIdentity = flag;
            }

            public void SetFlagCreate(bool flag)
            {
                flagCreate = flag;
            }
            public bool Create(string email, string password)
            {
                return flagCreate;
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public Task SetInitialData(ApplicationUser adminDto, List<string> roles)
            {
                throw new NotImplementedException();
            }

            public bool CreateStories(Stories item)
            {
                throw new NotImplementedException();
            }
        }
    }
}

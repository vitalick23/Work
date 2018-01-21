using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.BLL.Services;
using UserStories.BLL.Interfaces;
using UserStories.BLL.Entities;
using UserStories.WEB.Controllers;
using UserStories.WEB.Models;
using System.Web.Mvc;

namespace UserStories.WEB.Test
{
    [TestClass]
    class StoriesControllerTest
    {
        public FaceStorieService faceStorieService { get; private set; }
        public StoriesController controller { get; private set; }


        [TestInitialize]
        public void SetupContext()
        {
            faceStorieService = new FaceStorieService();
            controller = new StoriesController(faceStorieService);

        }

        #region CreateStoriesTest

        [TestMethod]
        public void CreateStoriesIfSuccess()
        {
            StoriesModel model = new StoriesModel();
            faceStorieService.SetFlagCreate(true);

            var result = controller.CreateStories(model) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateStoriesIfErrorModel()
        {
            StoriesModel model = new StoriesModel();
            controller.ModelState.AddModelError("Name", "Error");

            var result = controller.CreateStories(model) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("CreateStories",result.ViewName);
        }
        #endregion

    }

    public class FaceStorieService : IStoriesSevises
    {
        private bool flagCreate = false;

        public void SetFlagCreate(bool flag)
        {
            flagCreate = flag;
        }

        public bool Create(Stories item)
        {
            return flagCreate;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Stories> GetStories()
        {
            throw new NotImplementedException();
        }

        public Stories GetStories(string idStory)
        {
            throw new NotImplementedException();
        }

        public List<Stories> GetStoriesByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserStories.BLL.Entities;
using UserStories.BLL.Interfaces;
using UserStories.WEB.Models;

namespace UserStories.WEB.Controllers
{
    public class StoriesController : Controller
    {
        private readonly IStoriesSevises _storiesService;

        public StoriesController(IStoriesSevises storiesService)
        {
            _storiesService = storiesService;
        }
        
        public ActionResult Stories(string idStories)
        {
            var model = _storiesService.GetStories(idStories);
            return View(model);
        }

        public ActionResult CreateStories()
        {
            return View();
        }
      
        
        public ActionResult CreateStories(StoriesModel model)
        {
            if (!ModelState.IsValid)
            {
                model.IdUser = HttpContext.User.Identity.GetUserId();
                model.TimePublicate = DateTime.Now;
                if (_storiesService.Create((Stories)model))
                {
                    //leter add notification
                    return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            return View(model);
        }
    }
}
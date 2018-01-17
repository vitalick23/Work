using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserStories.WEB.Models;

namespace UserStories.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           // List<StoriesModel> model = new List<StoriesModel>();


            return View();
        }

       

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserStories.BLL.Interfaces;
using UserStories.WEB.Models;

namespace UserStories.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoriesSevises _storiesSrvice;

        //notest
        public HomeController (IStoriesSevises storiesSevises)
        {
            _storiesSrvice = storiesSevises;
        }
        public ActionResult Index()
        {
            var model = _storiesSrvice.GetStories();
            return View(model);
        }

       

    }
}
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.WebSockets;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using UserStories.BLL.DTO;
using UserStories.BLL.Interfaces;
using UserStories.BLL.Services;
using UserStories.WEB.Models;
using UserStories.BLL.Entities;
//see  home http://blog.byndyu.ru/p/blog-page_19.html
namespace UserStories.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        //спросить как реализовывать
        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser{ Email = model.Email, PasswordHash = model.Password };
                ClaimsIdentity claim =  _userService.Authenticate(user);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignIn(new AuthenticationProperties
                                                {
                                                    IsPersistent = true
                                                },
                                                    claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Register(RegisterModel model)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = (ApplicationUser)model;
               // var user = new ApplicationUser();
                if (!_userService.Create(user.Email, model.Password))
                    return View(model);
                 return RedirectToAction("Index", "Home");
           }
            return View(model);
        }

        
        
    }
}

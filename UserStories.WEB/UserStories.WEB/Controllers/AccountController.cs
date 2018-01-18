using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using Microsoft.AspNet.Identity.Owin;
using UserStories.BLL.DTO;
using UserStories.BLL.Interfaces;
using UserStories.BLL.Services;
using UserStories.WEB.Models;
using UserStories.BLL.Entities;

namespace UserStories.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;


        private IAuthenticationManager AuthenticationManager
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
        public async Task<ActionResult> Login(LoginModel model)
        {
            
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                                
                RegisterModel userDto = new RegisterModel { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim =  _userService.Authenticate(user);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
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
        public async Task<ActionResult> Register(RegisterModel model)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = (ApplicationUser)model;
               // var user = new ApplicationUser();
                _userService.Create(user,model.Password);
                 return RedirectToAction("Index", "Home");
           }
            return View(model);
        }
        //private async Task SetInitialDataAsync()
        //{
        //    await UserService.SetInitialData(new UserDTO
        //    {
        //        Email = "vitali_fc_arsenal@mail.ru",
        //        UserName = "vitali_fc_arsenal@mail.ru",
        //        Password = "S03an92!",
        //        Name = "Долговечный Виталий Николаевич",
        //        Address = "ул",
        //        Role = "admin",
        //    }, new List<string> { "user", "admin" });
        //}
    }
}

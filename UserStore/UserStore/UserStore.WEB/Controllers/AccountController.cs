using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using UserStore.Models;
using System.Security.Claims;
using UserStore.BLL.Interfaces;
using UserStore.BLL.Entities;

namespace UserStore.Controllers
{
    public class AccountController : Controller
    {
      
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
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
                ApplicationUser userDto = new ApplicationUser{ Email = model.Email, PasswordHash = model.Password};
                ClaimsIdentity claim = await _userService.Authenticate(userDto);
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
                ApplicationUser userDto = new ApplicationUser
                {
                    Email = model.Email,
                    PasswordHash = model.Password,
                };
                if( await _userService.Create(userDto))
                    return View("SuccessRegister");
                else
                    ModelState.AddModelError("Error","Error");
            }
            return View(model);
        }
        
    }
}
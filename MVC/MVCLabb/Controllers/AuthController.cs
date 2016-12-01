using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.Models;
using System.Security.Claims;
using MVCLabb.Data;
using MVCLabb.Data.Repository;
using MVCLabb.HelperMethods;
using MVCLabb.Mapper;

namespace MVCLabb.Controllers
{
    public class AuthController : Controller
    {
        IUserRepository userRepository;

        public AuthController()
        {
            userRepository = new UserRepository();
        }

        // GET: Auth
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = userRepository.ValidateLogin(model.Email, model.Password);

            if (user != null)
            {
                SetUpAuthCookie(user);
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            ModelState.AddModelError("", "Invalid email or passsword");
            return View(model);

        }

        private void SetUpAuthCookie(User user)
        {
            var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Admin ? "Admin" : "User")
                        },
                           "ApplicationCookie");

            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignIn(identity);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = UserMapper.MapRegistrationViewModel(model);
                userRepository.AddUser(user);
            }
            else
            {
                ModelState.AddModelError("", "Fill in all the fields and try again");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Manage()
        {
            var user = UserHelper.GetLogedInUser(userRepository);

            if (user != null)
            {
                ManageViewModel model = UserMapper.MapManageViewModel(user, userRepository);
                return View(model);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Manage(ManageViewModel model, FormCollection collection)
        {
            if (ModelState.IsValid)
            {

                User user = UserMapper.MapManageViewModel(model);
                user.Id = UserHelper.GetLogedInUser(userRepository).Id;

                userRepository.UpdateUser(user);

            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return Content(model.Name);
        }
    }
}
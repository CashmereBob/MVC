using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.Models;
using System.Security.Claims;
using MVCLabb.HelperMethods;

namespace MVCLabb.Controllers
{
    public class AuthController : Controller
    {
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

            if (UserHelper.LoginUser(model.Email, model.Password))
            {
                    return RedirectToAction("Index", "Home", new { area = "" });
            }

            ModelState.AddModelError("", "Invalid email or passsword");
            return View(model);
              
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
                var salt = UserHelper.CreateSalt(10);
                var password = UserHelper.GenerateSHA256Hash(model.Password, salt);


                using (var ctx = new MVCLabbEntities())
                {
                    ctx.tbl_User.Add(new tbl_User
                    {
                        Id = Guid.NewGuid(),
                        Email = model.Email,
                        Password = password,
                        Salt = salt,
                        Country = "nn",
                        Admin = false,
                        Name = model.Name

                    });

                    ctx.SaveChanges();
                }

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


            var user = UserHelper.GetLogedInUser();

            if (user != null) { 

                    var model = new ManageViewModel
                    {
                        Name = user.Name,
                        Country = user.Country,
                        Email = user.Email,
                        Password = user.Password,
                        ConfirmPassword = user.Password
                    };

                    return View(model);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Manage(ManageViewModel model, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                

                using (var ctx = new MVCLabbEntities())
                {
                    tbl_User user = ctx.tbl_User.Where(x => x.Email == model.Email).FirstOrDefault();
                    user.Name = model.Name;
                    user.Country = model.Country;
                    user.Email = model.Email;

                    if (user.Password != model.Password)
                    {
                        var salt = UserHelper.CreateSalt(10);
                        var password = UserHelper.GenerateSHA256Hash(model.Password, salt);

                        user.Salt = salt;
                        user.Password = password;

                    }
                    
                    ctx.SaveChanges();
                }

            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return View(model);
        }
    }
}
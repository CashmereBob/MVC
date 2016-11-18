using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.Models;
using System.Security.Claims;

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

            using (var dbCtx = new MVCLabbEntities())
            {

                foreach (var user in dbCtx.tbl_User)
                {
                    if (model.Email == user.Email && model.Password == user.Password)
                    {
                        var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                        },
                            "ApplicationCookie");

                        var ctx = Request.GetOwinContext();
                        var authManager = ctx.Authentication;

                        authManager.SignIn(identity);

                        return RedirectToAction("Index", "Home");
                    }
                }
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
                using (var ctx = new MVCLabbEntities())
                {
                    ctx.tbl_User.Add(new tbl_User
                    {
                        Id = Guid.NewGuid(),
                        Email = model.Email,
                        Password = model.Password,
                        Country = "nn",
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
            var claims = User.Identity as ClaimsIdentity;
            if (claims != null)
            {
                var userID = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                using (var ctx = new MVCLabbEntities())
                {
                    var user = ctx.tbl_User.FirstOrDefault(x => x.Id.ToString() == userID);

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
                    user.Password = model.Password;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLabb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(PhotoController.GetAllPhotosFromDb().Skip(Math.Max(0, PhotoController.GetAllPhotosFromDb().Count() - 3)));
        }
        [AllowAnonymous]
        public ActionResult Information()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Error()
        {
            return View();
        }
    }
}
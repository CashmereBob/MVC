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
        public ActionResult Index()
        {
            
            
            if (PhotoController.photoes != null)
            {
                return View(PhotoController.photoes.Skip(Math.Max(0, PhotoController.photoes.Count() - 3)));
            }

            return View();
        }

        public ActionResult Information()
        {
            return View();
        }
    }
}
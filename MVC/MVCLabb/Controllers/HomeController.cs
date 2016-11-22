using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.BI;
using MVCLabb.Mapper;
using MVCLabb.Models;

namespace MVCLabb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {

            var listPhotoFromDB = PhotoBI.GetAllPhotosFromDb().Skip(Math.Max(0, PhotoBI.GetAllPhotosFromDb().Count() - 3));
            var photos = new List<IndexPhotoViewModel>();
            listPhotoFromDB.ToList().ForEach(x => photos.Add(PhotoMapper.MapIndexPhotoViewModel(x)));

            return View(photos);   
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
using MVCLabb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.BI;
using MVCLabb.Mapper;
using MVCLabb.HelperMethods;

namespace MVCLabb.Controllers
{
    public class PhotoController : Controller
    {


        // GET: Photo
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<tbl_Photo> photosFromDB = PhotoBI.GetAllPhotosFromDb();
            List<IndexPhotoViewModel> photos = new List<IndexPhotoViewModel>();
            photosFromDB.ForEach(x => photos.Add(PhotoMapper.MapIndexPhotoViewModel(x)));

            return View(photos);
        }

        

        // GET: Photo/Details/5
        [AllowAnonymous]
        public ActionResult Details(DetailsPhotoViewModel photo)
        {
            tbl_Photo photoFromDB = PhotoBI.GetPhotoFromDbById(photo.Id);
            photo = PhotoMapper.MapDetailsPhotoViewModel(photoFromDB);

            return View(photo);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Details(DetailsPhotoViewModel photo, FormCollection collection)
        {
            var comment = new tbl_Comment {
                Date = DateTime.Now,
                Comment = collection["comment"],
                UserID = UserHelper.GetLogedInUser().Id,
                PhotoID = photo.Id
            };

            CommentBI.AddComment(comment);

            tbl_Photo photoFromDB = PhotoBI.GetPhotoFromDbById(photo.Id);
            photo = PhotoMapper.MapDetailsPhotoViewModel(photoFromDB);

            return View(photo);
        }


       

    }
}

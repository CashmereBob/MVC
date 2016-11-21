using MVCLabb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.HelperMethods;

namespace MVCLabb.Controllers
{
    public class PhotoController : Controller
    {


        // GET: Photo
        [AllowAnonymous]
        public ActionResult Index()
        {
            
                return View(GetAllPhotosFromDb());
            

        }

        public static List<IndexPhotoViewModel> GetAllPhotosFromDb()
        {
            using (var ctx = new MVCLabbEntities())
            {
                var model = new List<IndexPhotoViewModel>();
                ctx.tbl_Photo.ToList().ForEach(x =>
                model.Add(new IndexPhotoViewModel
                {
                    Id = x.Id,
                    Path = x.Path,
                    Name = x.Name
                }));

                return model;
            }
        }

        // GET: Photo/Details/5
        [AllowAnonymous]
        public ActionResult Details(DetailsPhotoViewModel photo)
        {
            
            return View(GetPhotoFromDb(photo));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Details(DetailsPhotoViewModel photo, FormCollection collection)
        {

            using (var ctx = new MVCLabbEntities())
            {
                var photoToUpdate = ctx.tbl_Photo.FirstOrDefault(x => x.Id == photo.Id);
                photoToUpdate.tbl_Comment.Add(new tbl_Comment
                {
                    Date = DateTime.Now,
                    Comment = collection["comment"],
                    UserID = UserHelper.GetLogedInUser().Id
                   
                });

                ctx.SaveChanges();
            }


            return View(GetPhotoFromDb(photo));
        }


        private DetailsPhotoViewModel GetPhotoFromDb(DetailsPhotoViewModel photo)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var photoFromDB = ctx.tbl_Photo.FirstOrDefault(x => x.Id == photo.Id);
                photo.Name = photoFromDB.Name;
                photo.Description = photoFromDB.Description;
                photo.Path = photoFromDB.Path;
                photo.Date = photoFromDB.Date;
                photo.UploaderName = photoFromDB.tbl_User.Name;
                photo.Album = photoFromDB.AlbumID != null ? photoFromDB.tbl_Album.Name : "Uncategorized";

                photoFromDB.tbl_Comment.ToList().ForEach(x =>
                photo.Comments.Add(new CommentViewModel
                {
                    id = x.Id,
                    email = x.tbl_User.Email,
                    name = x.tbl_User.Name,
                    date = x.Date,
                    comment = x.Comment
                }));

                return photo;
            }


        }

    }
}

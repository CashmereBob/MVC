using MVCLabb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        [AllowAnonymous]
        public ActionResult Details(DetailsPhotoViewModel photo, FormCollection collection)
        {

            using (var ctx = new MVCLabbEntities())
            {
                var photoToUpdate = ctx.tbl_Photo.FirstOrDefault(x => x.Id == photo.Id);
                photoToUpdate.tbl_Comment.Add(new tbl_Comment
                {
                    Name = collection["name"],
                    Email = collection["email"],
                    Comment = collection["comment"]
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

                photoFromDB.tbl_Comment.ToList().ForEach(x =>
                photo.Comments.Add(new CommentViewModel
                {
                    id = x.Id,
                    comment = x.Comment,
                    email = x.Email,
                    name = x.Name
                }));

                return photo;
            }


        }

    }
}

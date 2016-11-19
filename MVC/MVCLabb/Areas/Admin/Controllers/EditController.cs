using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.Areas.Admin.Models;
using MVCLabb.Controllers;
using System.IO;
using MVCLabb.Models;
using MVCLabb.HelperMethods;

namespace MVCLabb.Areas.Admin.Controllers
{
    public class EditController : Controller
    {
        Guid userID = UserHelper.GetLogedInUser().Id;

        // GET: Admin/Edit
        [Authorize]
        public ActionResult Index()
        {
            using (var ctx = new MVCLabbEntities())
            {
                IList<IndexPhotoViewModels> model = new List<IndexPhotoViewModels>();
                

                ctx.tbl_Photo.Where(x => x.UserID == userID).ToList().ForEach(x =>
                model.Add(new IndexPhotoViewModels {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Path = x.Path,
                    Comments = x.tbl_Comment.Count()
                })
                
                );

                return View(model);
            }

        }


        // GET: Admin/Edit/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Edit/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(CreatePhotoViewModels photo, HttpPostedFileBase photoUpload)
        {
            try
            {
                using (var ctx = new MVCLabbEntities())
                {
                    ctx.tbl_Photo.Add(new tbl_Photo {
                        Name = photo.Name,
                        Description = photo.Description,
                        Path = $" /Photos/{photoUpload.FileName}",
                        UserID = UserHelper.GetLogedInUser().Id
                    });

                    ctx.SaveChanges();
                }

                photoUpload.SaveAs(Path.Combine(Server.MapPath("~/Photos"), photoUpload.FileName));
                return RedirectToAction("Index");
            }
            catch
            {
                return View(photo);
            }
        }

        // GET: Admin/Edit/Edit/5
        public ActionResult Edit(EditPhotoViewModels photo)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var photoFromDB = ctx.tbl_Photo.FirstOrDefault(x => x.Id == photo.Id && x.UserID == userID);

                photo.Name = photoFromDB.Name;
                photo.Description = photoFromDB.Description;
                photo.Path = photoFromDB.Path;

                photoFromDB.tbl_Comment.ToList().ForEach(x =>
                photo.Comments.Add(new CommentViewModel {
                    id = x.Id,
                    name = x.Name,
                    email = x.Email,
                    comment = x.Comment
                }));

                return View(photo);
            }
                
        }

        // POST: Admin/Edit/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(EditPhotoViewModels photo, FormCollection collection)
        {
            try
            {

                using (var ctx = new MVCLabbEntities())
                {
                    var photoFromDb = ctx.tbl_Photo.FirstOrDefault(x => x.Id == photo.Id && x.UserID == userID);
                    photoFromDb.Name = photo.Name;
                    photoFromDb.Description = photo.Description;
                    ctx.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditComment(string id)
        {
            var photo = new EditPhotoViewModels();
            using (var ctx = new MVCLabbEntities())
            {
                
                ctx.tbl_Photo.FirstOrDefault(x => x.Id.ToString() == id && x.UserID == userID).tbl_Comment.ToList().ForEach(x =>
                photo.Comments.Add(new CommentViewModel {
                    name = x.Name,
                    id = x.Id,
                    comment = x.Comment,
                    email = x.Email
                }));
            }


                return View(photo.Comments);
        }


        // GET: Admin/Edit/Delete/5
        [Authorize]
        public ActionResult Delete(DeletePhotoViewModels photo)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var photoFromDb = ctx.tbl_Photo.FirstOrDefault(x => x.Id == photo.Id && x.UserID == userID);
                photo.Name = photoFromDb.Name;
                photo.Description = photoFromDb.Description;
                photo.Path = photoFromDb.Path;

                return View(photo);
            }
        }

        // POST: Admin/Edit/Delete/5
        [Authorize]
        [HttpPost]
        public ActionResult Delete(DeletePhotoViewModels photo, FormCollection collection)
        {
            try
            {
                using (var ctx = new MVCLabbEntities())
                {
                    var photoToDelete = ctx.tbl_Photo.FirstOrDefault(x => x.Id == photo.Id && x.UserID == userID);
                    ctx.tbl_Photo.Remove(photoToDelete);

                    ctx.SaveChanges();

                    string fullPath = Request.MapPath(photoToDelete.Path);

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

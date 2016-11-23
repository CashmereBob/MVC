using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.Areas.User.Models;
using MVCLabb.Controllers;
using System.IO;
using MVCLabb.BI;
using MVCLabb.HelperMethods;
using MVCLabb.Areas.User.Mapper;

namespace MVCLabb.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    public class PhotoController : Controller
    {
        Guid userID = UserHelper.GetLogedInUser().Id;

        // GET: User/Edit
        public ActionResult Index()
        {
            List<tbl_Photo> photosFromDB = PhotoBI.GetPhotoFromDbByUserId(userID);
            ICollection<IndexPhotoViewModels> model = new List<IndexPhotoViewModels>();
            photosFromDB.ForEach(x => model.Add(PhotoMapper.MapIndexPhotoViewModel(x)));

            return View(model);
            
        }


        // GET: User/Edit/Create
        public ActionResult Create()
        {
            var model = new CreatePhotoViewModels();
            var albums = AlbumBI.GettAllAlbumsByUserID(userID);
            model.Albums.Add(new SelectListItem { Text = "Uncategorized", Value = "0" });
            albums.ForEach(x => model.Albums.Add(new SelectListItem {Text = x.Name, Value = x.Id.ToString() }));

            return View(model);
        }

        // POST: User/Edit/Create
        [HttpPost]
        public ActionResult Create(CreatePhotoViewModels photo, HttpPostedFileBase photoUpload)
        {
            tbl_Photo photoToDB = PhotoMapper.MapCreatePhotoViewModel(photo);
            photoToDB.UserID = UserHelper.GetLogedInUser().Id;
            try
            {
                PhotoBI.AddPhotoToDBAndFolder(photoToDB, photoUpload);

                photoUpload.SaveAs(Path.Combine(Server.MapPath("~/Photos"), photoUpload.FileName));
                return RedirectToAction("Index");
            }
            catch
            {
                return View(photo);
            }
        }

        // GET: User/Edit/Edit/5
        public ActionResult Edit(EditPhotoViewModels photo)
        {
            tbl_Photo model = PhotoBI.GetPhotoFromDbById(photo.Id);
            photo = PhotoMapper.MapEditPhotoViewModel(model);

            if (model.UserID == userID)
                return View(photo);
            else
                return View();
                
        }

        // POST: User/Edit/Edit/5
        [HttpPost]
        public ActionResult Edit(EditPhotoViewModels photo, FormCollection collection)
        {
            tbl_Photo model = PhotoMapper.MapEditPhotoViewModel(photo);

            try
            {
                PhotoBI.UdaptePhoto(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/Delete/5
        public ActionResult Delete(DeletePhotoViewModels photo)
        {
            tbl_Photo model = PhotoBI.GetPhotoFromDbById(photo.Id);
            photo = PhotoMapper.MapDeletePhotoViewModel(model);

            if (model.UserID == userID)
            {
                return View(photo);
            }
            else
            {
                return View();
            }
        }

        // POST: User/Edit/Delete/5
        [HttpPost]
        public ActionResult Delete(DeletePhotoViewModels photo, FormCollection collection)
        {
            try
            {
                tbl_Photo model = PhotoBI.GetPhotoFromDbById(photo.Id);
                if (model.UserID == userID)
                { 
                PhotoBI.DeletePhotoFromDB(model);

                string fullPath = Request.MapPath(model.Path);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

    

    }
}

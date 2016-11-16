using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.Models;
using MVCLabb.Controllers;
using System.IO;

namespace MVCLabb.Areas.Admin.Controllers
{
    public class EditController : Controller
    {
        // GET: Admin/Edit
        [Authorize]
        public ActionResult Index()
        {
            if (PhotoController.photoes != null)
            {
                return View(PhotoController.photoes);
            }

            return View();
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
        public ActionResult Create(PhotoModel photo, HttpPostedFileBase photoUpload)
        {
            try
            {
                // TODO: Add insert logic here

                photoUpload.SaveAs(Path.Combine(Server.MapPath("~/Photos"), photoUpload.FileName));

                PhotoController.photoes.Add(new PhotoModel { name = photo.name, id = Guid.NewGuid(), path = $" /Photos/{photoUpload.FileName}" });

                return RedirectToAction("Index");
            }
            catch
            {
                return View(photo);
            }
        }

        // GET: Admin/Edit/Edit/5
        public ActionResult Edit(PhotoModel photo)
        {
            return View(PhotoController.photoes.Where(x => x.id == photo.id).FirstOrDefault());
        }

        // POST: Admin/Edit/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(PhotoModel photo, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                var updatePhoto = PhotoController.photoes.Where(x => x.id == photo.id).FirstOrDefault();

                updatePhoto.name = photo.name;
                updatePhoto.description = photo.description;


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/Delete/5
        [Authorize]
        public ActionResult Delete(PhotoModel photo)
        {
            return View(PhotoController.photoes.Where(x => x.id == photo.id).FirstOrDefault());
        }

        // POST: Admin/Edit/Delete/5
        [Authorize]
        [HttpPost]
        public ActionResult Delete(PhotoModel photo, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                string fullPath = Request.MapPath(PhotoController.photoes.Where(x => x.id == photo.id).FirstOrDefault().path);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    PhotoController.photoes.Remove(PhotoController.photoes.Where(x => x.id == photo.id).FirstOrDefault());
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

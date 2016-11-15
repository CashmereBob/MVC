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
        private static IList<PhotoModel> photoes = new List<PhotoModel>();
        // GET: Photo
        public ActionResult Index()
        {
            if (photoes.Count() < 1)
            {
                var photoDir = Directory.GetFiles(Server.MapPath("~/Photos"));
                
                foreach (var photo in photoDir)
                {
                    var info = new FileInfo(photo);

                    photoes.Add(new PhotoModel { name = info.Name, id = Guid.NewGuid(), path = $" /Photos/{info.Name}" });
                }
            }


            return View(photoes);
        }

        // GET: Photo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Photo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Photo/Create
        [HttpPost]
        public ActionResult Create(PhotoModel photo, HttpPostedFileBase photoUpload)
        {
            try
            {
                // TODO: Add insert logic here

                photoUpload.SaveAs(Path.Combine(Server.MapPath("~/Photos"), photoUpload.FileName));

                photoes.Add(new PhotoModel { name = photo.name, id = Guid.NewGuid(), path = $" /Photos/{photoUpload.FileName}"});

                return RedirectToAction("Index");
            }
            catch
            {
                return View(photo);
            }
        }

        // GET: Photo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Photo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Photo/Delete/5
        public ActionResult Delete(PhotoModel photo)
        {
            
            return View(photoes.Where(x => x.id == photo.id).FirstOrDefault());
        }

        // POST: Photo/Delete/5
        [HttpPost]
        public ActionResult Delete(PhotoModel photo, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                string fullPath = Request.MapPath(photoes.Where(x => x.id == photo.id).FirstOrDefault().path);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    photoes.Remove(photoes.Where(x => x.id == photo.id).FirstOrDefault());
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

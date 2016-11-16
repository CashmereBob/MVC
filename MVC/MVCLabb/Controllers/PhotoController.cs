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
        public static IList<PhotoModel> photoes = new List<PhotoModel>();

        // GET: Photo
        public ActionResult Index()
        {

            return View(photoes);
        }

        // GET: Photo/Details/5
        public ActionResult Details(PhotoModel photo)
        {
            return View(photoes.Where(x => x.id == photo.id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Details(PhotoModel photo, FormCollection collection)
        {
            var photoToUpdate = photoes.Where(x => x.id == photo.id).FirstOrDefault();

            photoToUpdate.comments.Add(new CommentModel {id = Guid.NewGuid(), name = collection["name"], email = collection["email"], comment = collection["comment"] });

            return View(photoToUpdate);
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

       

        
    }
}

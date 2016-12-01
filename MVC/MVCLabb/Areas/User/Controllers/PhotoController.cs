using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.Areas.User.Models;
using MVCLabb.Controllers;
using System.IO;
using MVCLabb.Data;
using MVCLabb.Data.Repository;
using MVCLabb.HelperMethods;
using MVCLabb.Areas.User.Mapper;

namespace MVCLabb.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    public class PhotoController : Controller
    {
        Guid userID;
        IUserRepository userRepository;
        IPhotoRepository photoRepository;
        IAlbumRepository albumRepository;

        public PhotoController()
        {
            userRepository = new UserRepository();
            photoRepository = new PhotoRepository();
            albumRepository = new AlbumRepository();
            userID = UserHelper.GetLogedInUser(userRepository).Id;

        }
        // GET: User/Edit
        public ActionResult Index()
        {
            List<Photo> photosFromDB = photoRepository.GetPhotoFromDbByUserId(userID);
            ICollection<IndexPhotoViewModels> model = new List<IndexPhotoViewModels>();
            photosFromDB.ForEach(x => model.Add(PhotoMapper.MapIndexPhotoViewModel(x)));

            return View(model);
            
        }


        // GET: User/Edit/Create
        public ActionResult Create()
        {
            var model = new CreatePhotoViewModels();
            var albums = albumRepository.GettAllAlbumsByUserID(userID);
            model.Albums.Add(new SelectListItem { Text = "Uncategorized", Value = "" });
            albums.ForEach(x => model.Albums.Add(new SelectListItem {Text = x.Name, Value = x.Id.ToString() }));

            return View(model);
        }

        // POST: User/Edit/Create
        [HttpPost]
        public ActionResult Create(CreatePhotoViewModels photo, HttpPostedFileBase photoUpload)
        {
            Photo photoToDB = PhotoMapper.MapCreatePhotoViewModel(photo);
            
            try
            {
                photoToDB.UserID = userID;
                photoToDB.Id = Guid.NewGuid();
                photoToDB.Date = DateTime.Now;
                photoToDB.Path = $" /Photos/{photoUpload.FileName}";
                photoRepository.AddPhotoToDB(photoToDB);

                photoUpload.SaveAs(Path.Combine(Server.MapPath("~/Photos"), photoUpload.FileName));
                return Content(photoUpload.FileName);
            }
            catch
            {
                var model = PhotoMapper.MapCreatePhotoViewModel(photoToDB);
                var albums = albumRepository.GettAllAlbumsByUserID(userID);
                model.Albums.Add(new SelectListItem { Text = "Uncategorized", Value = "" });
                albums.ForEach(x => model.Albums.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() }));

                return Content(photoUpload.FileName);
            }
        }

        // GET: User/Edit/Edit/5
        public ActionResult Edit(EditPhotoViewModels photo)
        {
            Photo model = photoRepository.GetPhotoFromDbById(photo.Id);
            photo = PhotoMapper.MapEditPhotoViewModel(model);
            photo.AlbumId = model.AlbumID != null ? (Guid)model.AlbumID : Guid.Empty;
            var albums = albumRepository.GettAllAlbumsByUserID(userID);
            photo.Albums.Add(new SelectListItem { Text = "Uncategorized", Value = Guid.Empty.ToString() });
            albums.ForEach(x => photo.Albums.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() }));

            if (model.UserID == userID)
                return View(photo);
            else
                return View();
                
        }

        // POST: User/Edit/Edit/5
        [HttpPost]
        public ActionResult Edit(EditPhotoViewModels photo, FormCollection collection)
        {
            Photo model = PhotoMapper.MapEditPhotoViewModel(photo);

            try
            {
                photoRepository.UdaptePhoto(model);
                return Content(photo.Name);
            }
            catch
            {
                return Content("Error");
            }
        }

        // GET: User/Edit/Delete/5
        public ActionResult Delete(DeletePhotoViewModels photo)
        {
            Photo model = photoRepository.GetPhotoFromDbById(photo.Id);
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
        public ActionResult Delete(string Id)
        {
            
            try
            {
                Photo model = photoRepository.GetPhotoFromDbById(new Guid(Id));
                if (model.UserID == userID)
                {
                    photoRepository.DeletePhotoFromDB(model);

                    string fullPath = Request.MapPath(model.Path);

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                }
                
            }
            catch
            {
                
            }
            List<Photo> photosFromDB = photoRepository.GetPhotoFromDbByUserId(userID);
            ICollection<IndexPhotoViewModels> models = new List<IndexPhotoViewModels>();
            photosFromDB.ForEach(x => models.Add(PhotoMapper.MapIndexPhotoViewModel(x)));


            return PartialView("_Images", models);
        }

    

    }
}

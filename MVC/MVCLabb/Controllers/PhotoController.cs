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

            return PartialView("_PhotoDetail", photo);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Details(DetailsPhotoViewModel photo, FormCollection collection)
        {
            var comment = new tbl_Comment
            {
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

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AlbumDetails(string id)
        {
            var album = AlbumBI.GetAlbumByID(id);
            DetailAlbumViewModel model = AlbumMapper.MapDetailAlbumViewModel(album);

            return PartialView("_AlbumDetails", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AlbumPhotoes(string id)
        {

            
                List<tbl_Photo> photosFromDB = PhotoBI.GetAllPhotoesInAlbumByID(id);
                List<IndexPhotoViewModel> photos = new List<IndexPhotoViewModel>();
                photosFromDB.ForEach(x => photos.Add(PhotoMapper.MapIndexPhotoViewModel(x)));
                return PartialView("_thumbnails", photos);


        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AlbumComments(string id)
        {
            List<tbl_Comment> comments = CommentBI.GetAllComentsByAlbumID(id);
            List<CommentViewModel> model = new List<CommentViewModel>();
            comments.ForEach(x => model.Add(CommentMapper.MapCommentViewModel(x)));



            return Json(model, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AlbumComments(string id, string comment)
        {
            Guid userID = UserHelper.GetLogedInUser().Id;

            var comm = new tbl_Comment {
                AlbumID = new Guid(id),
                Date = DateTime.Now,
                Comment = comment,
                Id = Guid.NewGuid(),
                UserID = userID
            };

            CommentBI.AddComment(comm);

            List<tbl_Comment> comments = CommentBI.GetAllComentsByAlbumID(id.ToString());
            List<CommentViewModel> model = new List<CommentViewModel>();
            comments.ForEach(x => model.Add(CommentMapper.MapCommentViewModel(x)));



            return Json(model);


        }
    }
}

using MVCLabb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.Data;
using MVCLabb.Data.Repository;
using MVCLabb.Mapper;
using MVCLabb.HelperMethods;

namespace MVCLabb.Controllers
{
    public class PhotoController : Controller
    {

        IUserRepository userRepository;
        IPhotoRepository photoRepository;
        IAlbumRepository albumRepository;
        ICommentRepository commentRepository;

        public PhotoController()
        {
            userRepository = new UserRepository();
            photoRepository = new PhotoRepository();
            albumRepository = new AlbumRepository();
            commentRepository = new CommentRepository();

        }
        // GET: Photo
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Photo> photosFromDB = photoRepository.GetAllPhotosFromDb();
            List<IndexPhotoViewModel> photos = new List<IndexPhotoViewModel>();
            photosFromDB.ForEach(x => photos.Add(PhotoMapper.MapIndexPhotoViewModel(x)));

            return View(photos);
        }



        // GET: Photo/Details/5
        [AllowAnonymous]
        public ActionResult Details(DetailsPhotoViewModel photo)
        {
            Photo photoFromDB = photoRepository.GetPhotoFromDbById(photo.Id);
            photo = PhotoMapper.MapDetailsPhotoViewModel(photoFromDB);

            return PartialView("_PhotoDetail", photo);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Details(DetailsPhotoViewModel photo, FormCollection collection)
        {
            var comment = new Comment
            {
                Date = DateTime.Now,
                Content = collection["comment"],
                UserID = UserHelper.GetLogedInUser(userRepository).Id,
                PhotoID = photo.Id
            };

            commentRepository.AddComment(comment);

            Photo photoFromDB = photoRepository.GetPhotoFromDbById(photo.Id);
            photo = PhotoMapper.MapDetailsPhotoViewModel(photoFromDB);

            return View(photo);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AlbumDetails(string id)
        {
            var album = albumRepository.GetAlbumByID(id);
            DetailAlbumViewModel model = AlbumMapper.MapDetailAlbumViewModel(album);

            return PartialView("_AlbumDetails", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AlbumPhotoes(string id)
        {

            
                List<Photo> photosFromDB = photoRepository.GetAllPhotoesInAlbumByID(id);
                List<IndexPhotoViewModel> photos = new List<IndexPhotoViewModel>();
                photosFromDB.ForEach(x => photos.Add(PhotoMapper.MapIndexPhotoViewModel(x)));
                return PartialView("_thumbnails", photos);


        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AlbumComments(string id)
        {
            List<Comment> comments = commentRepository.GetAllComentsByAlbumID(id);
            List<CommentViewModel> model = new List<CommentViewModel>();
            comments.ForEach(x => model.Add(CommentMapper.MapCommentViewModel(x)));



            return Json(model, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AlbumComments(string id, string comment)
        {
            Guid userID = UserHelper.GetLogedInUser(userRepository).Id;

            var comm = new Comment {
                AlbumID = new Guid(id),
                Date = DateTime.Now,
                Content = comment,
                Id = Guid.NewGuid(),
                UserID = userID
            };

            commentRepository.AddComment(comm);

            List<Comment> comments = commentRepository.GetAllComentsByAlbumID(id.ToString());
            List<CommentViewModel> model = new List<CommentViewModel>();
            comments.ForEach(x => model.Add(CommentMapper.MapCommentViewModel(x)));



            return Json(model);


        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult PhotoComments(string id)
        {
            List<Comment> comments = commentRepository.GetAllComentsByPhotoID(id);
            List<CommentViewModel> model = new List<CommentViewModel>();
            comments.ForEach(x => model.Add(CommentMapper.MapCommentViewModel(x)));



            return Json(model, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PhotoComments(string id, string comment)
        {
            Guid userID = UserHelper.GetLogedInUser(userRepository).Id;

            var comm = new Comment
            {
                PhotoID = new Guid(id),
                Date = DateTime.Now,
                Content = comment,
                Id = Guid.NewGuid(),
                UserID = userID
            };

            commentRepository.AddComment(comm);

            List<Comment> comments = commentRepository.GetAllComentsByAlbumID(id.ToString());
            List<CommentViewModel> model = new List<CommentViewModel>();
            comments.ForEach(x => model.Add(CommentMapper.MapCommentViewModel(x)));



            return Json(model);


        }
    }
}

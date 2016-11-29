using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.Data;
using MVCLabb.Data.Repository;
using MVCLabb.Mapper;
using MVCLabb.Models;

namespace MVCLabb.Controllers
{
    public class HomeController : Controller
    {
        IUserRepository userRepository;
        IPhotoRepository photoRepository;
        IAlbumRepository albumRepository;
        ICommentRepository commentRepository;

        public HomeController()
        {
            userRepository = new UserRepository();
            photoRepository = new PhotoRepository();
            albumRepository = new AlbumRepository();
            commentRepository = new CommentRepository();

        }

        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Information()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Media(string Search, string Filter)
        {

            if (Filter == "photo")
            {
                List<Photo> photosFromDB = photoRepository.GetSearchPhotosFromDb(Search);
                List<IndexPhotoViewModel> photos = new List<IndexPhotoViewModel>();
                photosFromDB.ForEach(x => photos.Add(PhotoMapper.MapIndexPhotoViewModel(x)));
                return PartialView("_thumbnails", photos);
            }
            else
            {
                List<Album> albumsFromDB = albumRepository.GetSearchAlbumsFromDB(Search).Where(x => x.Photos.Count() > 0).ToList();
                List<AlbumViewModel> albums = new List<AlbumViewModel>();
                albumsFromDB.ForEach(x => albums.Add(AlbumMapper.MapAlbumViewModel(x, photoRepository)));
                return PartialView("_thumbnailsAlbum", albums);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Data()
        {

            var model = new DataViewModel()
            {
                users = userRepository.GetAllUsers().Count().ToString(),
                albums = albumRepository.GettAllAlbums().Count().ToString(),
                comments = commentRepository.GetAllComments().Count().ToString(),
                photos = photoRepository.GetAllPhotosFromDb().Count().ToString(),
                latest = PhotoMapper.MapIndexPhotoViewModel(photoRepository.GetLastAddedPhoto()).Path
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
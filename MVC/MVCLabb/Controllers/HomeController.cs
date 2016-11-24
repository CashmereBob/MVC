using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.BI;
using MVCLabb.Mapper;
using MVCLabb.Models;

namespace MVCLabb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = new IndexViewModel()
            {
                Users = UserBI.GetAllUsers().Count(),
                Albums = AlbumBI.GettAllAlbums().Count(),
                Comments = CommentBI.GetAllComments().Count(),
                Photoes = PhotoBI.GetAllPhotosFromDb().Count(),
                Latest = PhotoMapper.MapIndexPhotoViewModel(PhotoBI.GetLastAddedPhoto())
            };


            return View(model);
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
                List<tbl_Photo> photosFromDB = PhotoBI.GetSearchPhotosFromDb(Search);
                List<IndexPhotoViewModel> photos = new List<IndexPhotoViewModel>();
                photosFromDB.ForEach(x => photos.Add(PhotoMapper.MapIndexPhotoViewModel(x)));
                return PartialView("_thumbnails", photos);
            }
            else
            {
                List<tbl_Album> albumsFromDB = AlbumBI.GetSearchAlbumsFromDB(Search);
                List<AlbumViewModel> albums = new List<AlbumViewModel>();
                albumsFromDB.ForEach(x => albums.Add(AlbumMapper.MapAlbumViewModel(x)));
                return PartialView("_thumbnailsAlbum", albums);
            }
        }
    }
}
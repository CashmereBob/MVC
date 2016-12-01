using MVCLabb.Areas.User.Models;
using MVCLabb.Areas.User.Mapper;
using MVCLabb.Data;
using MVCLabb.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.HelperMethods;

namespace MVCLabb.Areas.User.Controllers
{


    [Authorize(Roles = "User")]
    public class AlbumController : Controller
    {
        IUserRepository userRepository;
        IAlbumRepository albumRepository;
        Guid userID; 

        public AlbumController()
        {
            userRepository = new UserRepository();
            albumRepository = new AlbumRepository();
            userID = UserHelper.GetLogedInUser(userRepository).Id;

        }

        public ActionResult Index()
        {

            List<Album> albumsDB = albumRepository.GettAllAlbumsByUserID(userID);
            List<ListAlbumViewModel> albums = new List<ListAlbumViewModel>();
            albumsDB.ForEach(x => albums.Add(AlbumMapper.MapListAlbumViewModel(x)));
            return View(albums);


        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EditAlbumViewModel model)
        {
            if (ModelState.IsValid)
            {
                Album album = AlbumMapper.MapEditAlbumViewModel(model, userID);
                albumRepository.AddAlbum(album);
               
            }
            return Content(model.Name);
        }

        [HttpGet]
        public ActionResult Edit(EditAlbumViewModel model)
        {

            Album album = albumRepository.GetAlbumByID(model.Id);

            model = AlbumMapper.MapEditAlbumViewModel(album);

            if (album.UserID == userID)
            {
                return View(model);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Edit(EditAlbumViewModel model, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                Album album = AlbumMapper.MapEditAlbumViewModel(model, userID);

                albumRepository.UpdateAlbum(album);
                
            }
            return Content(model.Name);
        }

        [HttpGet]
        public ActionResult Delete(EditAlbumViewModel model)
        {
            Album album = albumRepository.GetAlbumByID(model.Id);

            model = AlbumMapper.MapEditAlbumViewModel(album);

            if (album.UserID == userID)
            {
                return View(model);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Delete(EditAlbumViewModel model, FormCollection collection)
        {
            Album album = AlbumMapper.MapEditAlbumViewModel(model, userID);
            albumRepository.DeleteAlbum(album);

            List<Album> albumsDB = albumRepository.GettAllAlbumsByUserID(userID);
            List<ListAlbumViewModel> albums = new List<ListAlbumViewModel>();
            albumsDB.ForEach(x => albums.Add(AlbumMapper.MapListAlbumViewModel(x)));

            return PartialView("_Items", albums);
        }
    }
}
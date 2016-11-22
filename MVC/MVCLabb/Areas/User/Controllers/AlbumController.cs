using MVCLabb.Areas.User.Models;
using MVCLabb.Areas.User.Mapper;
using MVCLabb.BI;
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
        Guid userID = UserHelper.GetLogedInUser().Id;
        public ActionResult Index()
        {

            List<tbl_Album> albumsDB = AlbumBI.GettAllAlbumsByUserID(userID);
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
                tbl_Album album = AlbumMapper.MapEditAlbumViewModel(model, userID);
                AlbumBI.AddAlbum(album);
               
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(EditAlbumViewModel model)
        {

            tbl_Album album = AlbumBI.GetAlbumByID(model.Id);

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
                tbl_Album album = AlbumMapper.MapEditAlbumViewModel(model, userID);

                AlbumBI.UpdateAlbum(album);
                
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(EditAlbumViewModel model)
        {
            tbl_Album album = AlbumBI.GetAlbumByID(model.Id);

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
            tbl_Album album = AlbumMapper.MapEditAlbumViewModel(model, userID);
            AlbumBI.DeleteAlbum(album);
            return RedirectToAction("Index");
        }
    }
}
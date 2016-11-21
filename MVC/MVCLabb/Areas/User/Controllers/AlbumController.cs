using MVCLabb.Areas.User.Models;
using MVCLabb.HelperMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLabb.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    public class AlbumController : Controller
    {
        Guid userID = UserHelper.GetLogedInUser().Id;
        public ActionResult Index()
        {
            using (var ctx = new MVCLabbEntities())
            {
                IList<ListAlbumViewModel> model = new List<ListAlbumViewModel>();

                ctx.tbl_Album.Where(x => x.tbl_User.Id == userID).ToList().ForEach(x =>
                model.Add(new ListAlbumViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Name = x.Name,
                    Photos = x.tbl_Photo.Count()
                }));

                return View(model);
            }

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EditAlbumViewModel model)
        {
            try
            {
                using (var ctx = new MVCLabbEntities())
                {
                    ctx.tbl_Album.Add(new tbl_Album
                    {
                        Name = model.Name,
                        Description = model.Description,
                        UserID = userID

                    });

                    ctx.SaveChanges();
                }
                return RedirectToAction("ListAlbums");
            }
            catch
            {
                return View(model);
            }


        }

        [HttpGet]
        public ActionResult Edit(EditAlbumViewModel model)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var albumFromDb = ctx.tbl_Album.FirstOrDefault(x => x.Id == model.Id && x.UserID == userID);

                model.Name = albumFromDb.Name;
                model.Description = albumFromDb.Description;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditAlbumViewModel model, FormCollection collection)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var albumFromDb = ctx.tbl_Album.FirstOrDefault(x => x.Id == model.Id && x.UserID == userID);
                albumFromDb.Name = model.Name;
                albumFromDb.Description = model.Description;

                ctx.SaveChanges();
            }
            return RedirectToAction("ListAlbums");
        }

        [HttpGet]
        public ActionResult Delete(EditAlbumViewModel model)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var albumFromDb = ctx.tbl_Album.FirstOrDefault(x => x.Id == model.Id && x.UserID == userID);

                model.Name = albumFromDb.Name;
                model.Description = albumFromDb.Description;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(EditAlbumViewModel model, FormCollection collection)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var albumFromDb = ctx.tbl_Album.FirstOrDefault(x => x.Id == model.Id && x.UserID == userID);
                ctx.tbl_Album.Remove(albumFromDb);

                ctx.SaveChanges();
            }
            return RedirectToAction("ListAlbums");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.Models;
using MVCLabb.HelperMethods;

namespace MVCLabb.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    public class CommentController : Controller
    {
        Guid userID = UserHelper.GetLogedInUser().Id;
        // GET: User/Comment
        public ActionResult Index(string id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                IList<CommentViewModel> model = new List<CommentViewModel>();

                ctx.tbl_Photo.FirstOrDefault(x => x.Id.ToString() == id && x.UserID == userID).tbl_Comment.ToList().ForEach(x =>
                model.Add(new CommentViewModel {
                    id = x.Id,
                    comment = x.Comment,
                    date = x.Date,
                    email = x.tbl_User.Email,
                    name = x.tbl_User.Name
                }));

                return View(model);
            }
           
        }

        [HttpGet]
        public ActionResult Delete(CommentViewModel model)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var commentFromDB = ctx.tbl_Comment.FirstOrDefault(x => x.Id == model.id);

                

                model.comment = commentFromDB.Comment;
                model.date = commentFromDB.Date;
                model.email = commentFromDB.tbl_User.Email;
                model.name = commentFromDB.tbl_User.Name;
                return View(model);
            }

            
        }

        [HttpPost]
        public ActionResult Delete(CommentViewModel model, FormCollection collection)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var commentFromDB = ctx.tbl_Comment.FirstOrDefault(x => x.Id == model.id);

                ctx.tbl_Comment.Remove(commentFromDB);

                ctx.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}
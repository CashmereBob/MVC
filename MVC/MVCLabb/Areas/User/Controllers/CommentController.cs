using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.Models;
using MVCLabb.BI;
using MVCLabb.HelperMethods;
using MVCLabb.Mapper;

namespace MVCLabb.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    public class CommentController : Controller
    {
        Guid userID = UserHelper.GetLogedInUser().Id;
        // GET: User/Comment
        public ActionResult Index(string id)
        {
            List<tbl_Comment> comments = CommentBI.GetAllComentsByPhotoID(id);
            List<CommentViewModel> model = new List<CommentViewModel>();
            comments.ForEach(x => model.Add(CommentMapper.MapCommentViewModel(x)));

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(CommentViewModel model)
        {
            tbl_Comment comment = CommentBI.GetCommentByID(model.id);

            model = CommentMapper.MapCommentViewModel(comment);

            if (comment.tbl_Photo.UserID == userID)
            {
                return View(model);
            }
            return View();

        }

        [HttpPost]
        public ActionResult Delete(CommentViewModel model, FormCollection collection)
        {
            var comment = CommentBI.GetCommentByID(model.id);

            CommentBI.DeleteComment(comment);

            return RedirectToAction("Index", new { id=comment.PhotoID });

        }
    }
}
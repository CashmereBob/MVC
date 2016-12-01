using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.Models;
using MVCLabb.Data;
using MVCLabb.Data.Repository;
using MVCLabb.HelperMethods;
using MVCLabb.Mapper;

namespace MVCLabb.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    public class CommentController : Controller
    {
        Guid userID;
        IUserRepository userRepository;
        ICommentRepository commentRepository;

        public CommentController()
        {
            userRepository = new UserRepository();
            commentRepository = new CommentRepository();
            userID = UserHelper.GetLogedInUser(userRepository).Id;

        }

        // GET: User/Comment
        public ActionResult Index(string id)
        {
            List<Comment> comments = commentRepository.GetAllComentsByPhotoID(id);
            List<CommentViewModel> model = new List<CommentViewModel>();
            comments.ForEach(x => model.Add(CommentMapper.MapCommentViewModel(x)));

            return View(model);
        }

        public ActionResult IndexAlbum(string id)
        {
            List<Comment> comments = commentRepository.GetAllComentsByAlbumID(id);
            List<CommentViewModel> model = new List<CommentViewModel>();
            comments.ForEach(x => model.Add(CommentMapper.MapCommentViewModel(x)));

            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Delete(CommentViewModel model)
        {
            Comment comment = commentRepository.GetCommentByID(model.id);

            model = CommentMapper.MapCommentViewModel(comment);


            var photoid = comment.Photo != null ? comment.Photo.UserID : Guid.Empty;
            var albumid = comment.Album != null ? comment.Album.UserID : Guid.Empty;

            if (photoid == userID || albumid == userID)
            {
                return View(model);
            }
            return View();

        }

        [HttpPost]
        public ActionResult Delete(CommentViewModel model, FormCollection collection)
        {
            var comment = commentRepository.GetCommentByID(model.id);

            commentRepository.DeleteComment(comment);

            Guid id;
            List<CommentViewModel> models = new List<CommentViewModel>();
            if (comment.AlbumID == null ) {
                id = (Guid)comment.PhotoID;
                List<Comment> comments = commentRepository.GetAllComentsByPhotoID(id.ToString());
                comments.ForEach(x => models.Add(CommentMapper.MapCommentViewModel(x)));
            } else
            {
                id = (Guid)comment.AlbumID;
                List<Comment> comments = commentRepository.GetAllComentsByAlbumID(id.ToString());
                comments.ForEach(x => models.Add(CommentMapper.MapCommentViewModel(x)));
            }

            return PartialView("_Items", models);

        }
    }
}
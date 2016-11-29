using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Repository
{
    public class CommentRepository : ICommentRepository
    {
        public void AddComment(Comment comment)
        {
            using (var ctx = new MVCLabbEntities())
            {
                comment.Id = Guid.NewGuid();
                ctx.Comments.Add(comment);
                ctx.SaveChanges();
            }
        }

        public void DeleteComment(Comment comment)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var commentDB = ctx.Comments.FirstOrDefault(x => x.Id == comment.Id);
                ctx.Comments.Remove(commentDB);


                ctx.SaveChanges();
            }
        }

        public List<Comment> GetAllComentsByAlbumID(string id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Comments.Include("User").Include("Photo").Where(x => x.AlbumID.ToString() == id).ToList();
            }
        }

        public List<Comment> GetAllComentsByPhotoID(string id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Comments.Include("User").Include("Photo").Include("Album").Where(x => x.PhotoID.ToString() == id).ToList();
            }
        }

        public List<Comment> GetAllComments()
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Comments.Include("User").Include("Photo").ToList();
            }
        }

        public Comment GetCommentByID(Guid id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Comments.Include("User").Include("Photo").Include("Album").FirstOrDefault(x => x.Id == id);
            }
        }
    }
}

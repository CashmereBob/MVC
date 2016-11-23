using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLabb.BI
{
    public static class CommentBI
    {
        public static void AddComment(tbl_Comment comment)
        {
            using (var ctx = new MVCLabbEntities())
            {
                comment.Id = Guid.NewGuid();
                ctx.tbl_Comment.Add(comment);
                ctx.SaveChanges();
            }
        }

        internal static List<tbl_Comment> GetAllComentsByPhotoID(string id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Photo.Include("tbl_Comment").Include("tbl_User").FirstOrDefault(x => x.Id.ToString() == id).tbl_Comment.ToList();
            }
        }

        internal static void DeleteComment(tbl_Comment comment)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var commentDB = ctx.tbl_Comment.FirstOrDefault(x => x.Id == comment.Id);
                ctx.tbl_Comment.Remove(commentDB);
               

                ctx.SaveChanges();
            }
        }

        internal static tbl_Comment GetCommentByID(Guid id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Comment.Include("tbl_User").Include("tbl_Photo").FirstOrDefault(x => x.Id == id);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Models;

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
                return ctx.tbl_Comment.Include("tbl_User").Include("tbl_Photo").Include("tbl_Album").Where(x => x.PhotoID.ToString() == id).ToList();
            }
        }

        internal static List<tbl_Comment> GetAllComments()
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Comment.Include("tbl_User").Include("tbl_Photo").ToList();
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
                return ctx.tbl_Comment.Include("tbl_User").Include("tbl_Photo").Include("tbl_Album").FirstOrDefault(x => x.Id == id);
            }
        }

        internal static List<tbl_Comment> GetAllComentsByAlbumID(string id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Comment.Include("tbl_User").Include("tbl_Photo").Where(x => x.AlbumID.ToString() == id).ToList();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Models;
namespace MVCLabb.BI
{
    public static class PhotoBI
    {
        public static List<string> GetListOfAlbumsFromListOfPhotos(IEnumerable<IndexPhotoViewModel> photoes)
        {
            var result = new List<string>();

            foreach (var photo in photoes)
            {
                if (!result.Contains(photo.Album))
                {
                    result.Add(photo.Album);
                }
            }

            return result;
        }

        public static List<tbl_Photo> GetAllPhotosFromDb()
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Photo.ToList();
            }
        }

        internal static tbl_Photo GetPhotoFromDbById(Guid id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Photo.Include("tbl_User").Include("tbl_Album").Include("tbl_Comment").FirstOrDefault(x => x.Id == id);
            }
        }

        internal static void UpdatePhoto(tbl_Photo photoToDB)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var photo = ctx.tbl_Photo.FirstOrDefault(x => x.Id == photoToDB.Id);
                photo.tbl_Comment = photoToDB.tbl_Comment;

                ctx.SaveChanges();
            }
        }
    }
}
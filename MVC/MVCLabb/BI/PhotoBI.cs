using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Models;
using System.IO;

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

        internal static tbl_Photo GetLastAddedInAlbum(tbl_Album album)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Photo.Include("tbl_User").Include("tbl_Album").Include("tbl_Comment").Where(x => x.AlbumID == album.Id).OrderByDescending(x => x.Date).FirstOrDefault();
            }
        }

        internal static List<tbl_Photo> GetPhotoFromDbByUserId(Guid userID)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Photo.Include("tbl_User").Include("tbl_Album").Include("tbl_Comment").Where(x => x.UserID == userID).ToList();
            }
        }

        internal static tbl_Photo GetLastAddedPhoto()
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Photo.Include("tbl_User").Include("tbl_Album").Include("tbl_Comment").OrderByDescending(x => x.Date).FirstOrDefault();
            }
        }

        internal static List<tbl_Photo> GetSearchPhotosFromDb(string search)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Photo.Include("tbl_User").Include("tbl_Album").Include("tbl_Comment").Where(x =>
                x.Name.Contains(search) || x.Description.Contains(search) || x.tbl_Album.Name.Contains(search)).ToList();
            }
        }

        public static List<tbl_Photo> GetAllPhotosFromDb()
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Photo.Include("tbl_User").Include("tbl_Album").Include("tbl_Comment").ToList();
            }
        }

        internal static tbl_Photo GetPhotoFromDbById(Guid id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Photo.Include("tbl_User").Include("tbl_Album").Include("tbl_Comment").FirstOrDefault(x => x.Id == id);
            }
        }

        internal static void AddPhotoToDB(tbl_Photo photoToDB)
        {
            

            photoToDB.AlbumID = photoToDB.AlbumID == Guid.Empty ? null : photoToDB.AlbumID;

            using (var ctx = new MVCLabbEntities())
            {
                ctx.tbl_Photo.Add(photoToDB);

                ctx.SaveChanges();
            }


        }

        internal static void UdaptePhoto(tbl_Photo photo)
        {

            photo.AlbumID = photo.AlbumID == Guid.Empty ? null : photo.AlbumID;
            using (var ctx = new MVCLabbEntities())
            {
                var photoFromDb = ctx.tbl_Photo.FirstOrDefault(x => x.Id == photo.Id);
                photoFromDb.Name = photo.Name;
                photoFromDb.Description = photo.Description;
                photoFromDb.AlbumID = photo.AlbumID;
                ctx.SaveChanges();
            }
        }

        internal static void DeletePhotoFromDB(tbl_Photo model)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var photoToDelete = ctx.tbl_Photo.FirstOrDefault(x => x.Id == model.Id);
                ctx.tbl_Photo.Remove(photoToDelete);

                ctx.SaveChanges();

            }
        }
    }
}
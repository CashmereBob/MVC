using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        public void AddPhotoToDB(Photo photoToDB)
        {
            photoToDB.AlbumID = photoToDB.AlbumID == Guid.Empty ? null : photoToDB.AlbumID;

            using (var ctx = new MVCLabbEntities())
            {
                ctx.Photos.Add(photoToDB);

                ctx.SaveChanges();
            }
        }

        public void DeletePhotoFromDB(Photo model)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var photoToDelete = ctx.Photos.FirstOrDefault(x => x.Id == model.Id);
                ctx.Photos.Remove(photoToDelete);

                ctx.SaveChanges();

            }
        }

        public List<Photo> GetAllPhotoesInAlbumByID(string id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Photos.Include("User").Include("Album").Include("Comments").Where(x => x.AlbumID.ToString() == id).ToList();
            }
        }

        public List<Photo> GetAllPhotosFromDb()
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Photos.Include("User").Include("Album").Include("Comments").ToList();
            }
        }

        public Photo GetLastAddedInAlbum(Album album)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Photos.Include("User").Include("Album").Include("Comments").Where(x => x.AlbumID == album.Id).OrderByDescending(x => x.Date).FirstOrDefault();
            }
        }

        public Photo GetLastAddedPhoto()
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Photos.Include("User").Include("Album").Include("Comments").OrderByDescending(x => x.Date).FirstOrDefault();
            }
        }

        public Photo GetPhotoFromDbById(Guid id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Photos.Include("User").Include("Album").Include("Comments").FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Photo> GetPhotoFromDbByUserId(Guid userID)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Photos.Include("User").Include("Album").Include("Comments").Where(x => x.UserID == userID).ToList();
            }
        }

        public List<Photo> GetSearchPhotosFromDb(string search)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Photos.Include("User").Include("Album").Include("Comments").Where(x =>
                x.Name.Contains(search) || x.Description.Contains(search) || x.Album.Name.Contains(search)).ToList();
            }
        }

        public void UdaptePhoto(Photo photo)
        {
            photo.AlbumID = photo.AlbumID == Guid.Empty ? null : photo.AlbumID;
            using (var ctx = new MVCLabbEntities())
            {
                var photoFromDb = ctx.Photos.FirstOrDefault(x => x.Id == photo.Id);
                photoFromDb.Name = photo.Name;
                photoFromDb.Description = photo.Description;
                photoFromDb.AlbumID = photo.AlbumID;
                ctx.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Models;
using MVCLabb.Data;
using MVCLabb.Data.Repository;

namespace MVCLabb.Mapper
{
    public class PhotoMapper
    {
        internal static DetailsPhotoViewModel MapDetailsPhotoViewModel(Photo photoFromDB)
        {
            var photo = new DetailsPhotoViewModel();

            photo.Name = photoFromDB.Name;
            photo.Description = photoFromDB.Description;
            photo.Path = photoFromDB.Path;
            photo.Date = photoFromDB.Date;
            photo.UploaderName = photoFromDB.User.Name;
            photo.Album = photoFromDB.AlbumID != null ? photoFromDB.Album.Name : "Uncategorized";

            photoFromDB.Comments.ToList().ForEach(x =>
            photo.Comments.Add(new CommentViewModel
            {
                id = x.Id,
                email = x.User.Email,
                name = x.User.Name,
                date = x.Date,
                comment = x.Content
            }));

            return photo;
            
        }

        internal static ICollection<IndexPhotoViewModel> MapIndexPhotoViewModel(ICollection<Photo> photos)
        {
            var result = new List<IndexPhotoViewModel>();
            photos.ToList().ForEach(x => result.Add(MapIndexPhotoViewModel(x)));
            return result;
        }

        internal static IndexPhotoViewModel MapIndexPhotoViewModel(Photo photoFromDB)
        {
            return new IndexPhotoViewModel
            {
                Id = photoFromDB.Id,
                Path = photoFromDB.Path,
                Name = photoFromDB.Name
            };

           
        }

        internal static Photo MapDetailsPhotoViewModel(DetailsPhotoViewModel photoView, IPhotoRepository PhotoBI)
        {
            var photo = PhotoBI.GetPhotoFromDbById(photoView.Id);

            photo.Description = photoView.Description;
            photo.Name = photoView.Name;
            photo.Path = photoView.Path;

            photoView.Comments.ToList().ForEach(x => 
            photo.Comments.Add(new Comment {
                Content = x.comment,
                Date = x.date,
                UserID = x.userID
            }));

            return photo;

           
        }
    }
}
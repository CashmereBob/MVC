using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Models;
using MVCLabb.BI;

namespace MVCLabb.Mapper
{
    public class PhotoMapper
    {
        internal static DetailsPhotoViewModel MapDetailsPhotoViewModel(tbl_Photo photoFromDB)
        {
            var photo = new DetailsPhotoViewModel();

            photo.Name = photoFromDB.Name;
            photo.Description = photoFromDB.Description;
            photo.Path = photoFromDB.Path;
            photo.Date = photoFromDB.Date;
            photo.UploaderName = photoFromDB.tbl_User.Name;
            photo.Album = photoFromDB.AlbumID != null ? photoFromDB.tbl_Album.Name : "Uncategorized";

            photoFromDB.tbl_Comment.ToList().ForEach(x =>
            photo.Comments.Add(new CommentViewModel
            {
                id = x.Id,
                email = x.tbl_User.Email,
                name = x.tbl_User.Name,
                date = x.Date,
                comment = x.Comment
            }));

            return photo;
            
        }

        internal static ICollection<IndexPhotoViewModel> MapIndexPhotoViewModel(ICollection<tbl_Photo> photos)
        {
            var result = new List<IndexPhotoViewModel>();
            photos.ToList().ForEach(x => result.Add(MapIndexPhotoViewModel(x)));
            return result;
        }

        internal static IndexPhotoViewModel MapIndexPhotoViewModel(tbl_Photo photoFromDB)
        {
            return new IndexPhotoViewModel
            {
                Id = photoFromDB.Id,
                Path = photoFromDB.Path,
                Name = photoFromDB.Name
            };

           
        }

        internal static tbl_Photo MapDetailsPhotoViewModel(DetailsPhotoViewModel photoView)
        {
            var photo = PhotoBI.GetPhotoFromDbById(photoView.Id);

            photo.Description = photoView.Description;
            photo.Name = photoView.Name;
            photo.Path = photoView.Path;

            photoView.Comments.ToList().ForEach(x => 
            photo.tbl_Comment.Add(new tbl_Comment {
                Comment = x.comment,
                Date = x.date,
                UserID = x.userID
            }));

            return photo;

           
        }
    }
}
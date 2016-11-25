using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Areas.User.Models;
using MVCLabb.Models;

namespace MVCLabb.Areas.User.Mapper
{
    public class PhotoMapper
    {
        internal static IndexPhotoViewModels MapIndexPhotoViewModel(tbl_Photo photoFromDB)
        {
            return new IndexPhotoViewModels
            {
                Id = photoFromDB.Id,
                Path = photoFromDB.Path,
                Name = photoFromDB.Name,
                Description = photoFromDB.Description,
                Comments = photoFromDB.tbl_Comment != null ? photoFromDB.tbl_Comment.Count() : 0,
                Album = photoFromDB.AlbumID != null ? photoFromDB.tbl_Album.Name : "Uncatogorized"
            };
        }

        internal static tbl_Photo MapCreatePhotoViewModel(CreatePhotoViewModels photo)
        {
            return new tbl_Photo
            {
                Name = photo.Name,
                Description = photo.Description,
                AlbumID = photo.AlbumId != null ? photo.AlbumId : Guid.Empty
            };
        }

        internal static EditPhotoViewModels MapEditPhotoViewModel(tbl_Photo model)
        {
            var photo = new EditPhotoViewModels();

            photo.Name = model.Name;
            photo.Description = model.Description;
            photo.Path = model.Path;
            photo.Id = model.Id;

            model.tbl_Comment.ToList().ForEach(x =>
            photo.Comments.Add(new CommentViewModel
            {
                id = x.Id,
                comment = x.Comment
            }));

            return photo;
        }

        internal static tbl_Photo MapEditPhotoViewModel(EditPhotoViewModels photo)
        {
            return new tbl_Photo
            {
                Description = photo.Description,
                Name = photo.Name,
                Id = photo.Id,
                AlbumID = photo.AlbumId != null ? photo.AlbumId : Guid.Empty
            };
        }

        internal static DeletePhotoViewModels MapDeletePhotoViewModel(tbl_Photo model)
        {
            return new DeletePhotoViewModels
            {
                Description = model.Description,
                Id = model.Id,
                Name = model.Name,
                Path = model.Path
            };
        }

        internal static CreatePhotoViewModels MapCreatePhotoViewModel(tbl_Photo photo)
        {
            return new CreatePhotoViewModels
            {
                Name = photo.Name,
                Description = photo.Description,
                AlbumId = photo.AlbumID != null ? (Guid)photo.AlbumID : Guid.Empty

            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Areas.User.Models;
using MVCLabb.Models;
using MVCLabb.Data;

namespace MVCLabb.Areas.User.Mapper
{
    public class PhotoMapper
    {
        internal static IndexPhotoViewModels MapIndexPhotoViewModel(Photo photoFromDB)
        {
            return new IndexPhotoViewModels
            {
                Id = photoFromDB.Id,
                Path = photoFromDB.Path,
                Name = photoFromDB.Name,
                Description = photoFromDB.Description,
                Comments = photoFromDB.Comments != null ? photoFromDB.Comments.Count() : 0,
                Album = photoFromDB.AlbumID != null ? photoFromDB.Album.Name : "Uncatogorized"
            };
        }

        internal static Photo MapCreatePhotoViewModel(CreatePhotoViewModels photo)
        {
            return new Photo
            {
                Name = photo.Name,
                Description = photo.Description,
                AlbumID = photo.AlbumId != null ? photo.AlbumId : Guid.Empty
            };
        }

        internal static EditPhotoViewModels MapEditPhotoViewModel(Photo model)
        {
            var photo = new EditPhotoViewModels();

            photo.Name = model.Name;
            photo.Description = model.Description;
            photo.Path = model.Path;
            photo.Id = model.Id;

            model.Comments.ToList().ForEach(x =>
            photo.Comments.Add(new CommentViewModel
            {
                id = x.Id,
                comment = x.Content
            }));

            return photo;
        }

        internal static Photo MapEditPhotoViewModel(EditPhotoViewModels photo)
        {
            return new Photo
            {
                Description = photo.Description,
                Name = photo.Name,
                Id = photo.Id,
                AlbumID = photo.AlbumId != null ? photo.AlbumId : Guid.Empty
            };
        }

        internal static DeletePhotoViewModels MapDeletePhotoViewModel(Photo model)
        {
            return new DeletePhotoViewModels
            {
                Description = model.Description,
                Id = model.Id,
                Name = model.Name,
                Path = model.Path
            };
        }

        internal static CreatePhotoViewModels MapCreatePhotoViewModel(Photo photo)
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
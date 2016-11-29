using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Areas.User.Models;
using MVCLabb.Data;

namespace MVCLabb.Areas.User.Mapper
{
    public class AlbumMapper
    {
        internal static ListAlbumViewModel MapListAlbumViewModel(Album albums)
        {
            return new ListAlbumViewModel
            {
                Id = albums.Id,
                Description = albums.Description,
                Name = albums.Name,
                Photos = albums.Photos.Count()
            };
        }

        internal static Album MapEditAlbumViewModel(EditAlbumViewModel model, Guid id)
        {
            return new Album
            {
                Name = model.Name,
                Description = model.Description,
                UserID = id,
                Id = model.Id
            };
        }

        internal static EditAlbumViewModel MapEditAlbumViewModel(Album album)
        {
            return new EditAlbumViewModel
            {
                Id = album.Id,
                Description = album.Description,
                Name = album.Name
               
            };
        }
    }
}
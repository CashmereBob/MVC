using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Areas.User.Models;

namespace MVCLabb.Areas.User.Mapper
{
    public class AlbumMapper
    {
        internal static ListAlbumViewModel MapListAlbumViewModel(tbl_Album albums)
        {
            return new ListAlbumViewModel
            {
                Id = albums.Id,
                Description = albums.Description,
                Name = albums.Name,
                Photos = albums.tbl_Photo.Count()
            };
        }

        internal static tbl_Album MapEditAlbumViewModel(EditAlbumViewModel model, Guid id)
        {
            return new tbl_Album
            {
                Name = model.Name,
                Description = model.Description,
                UserID = id,
                Id = model.Id
            };
        }

        internal static EditAlbumViewModel MapEditAlbumViewModel(tbl_Album album)
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
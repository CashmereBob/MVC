using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Models;
using MVCLabb.BI;

namespace MVCLabb.Mapper
{
    public static class AlbumMapper
    {
        internal static AlbumViewModel MapAlbumViewModel(tbl_Album album)
        {
            return new AlbumViewModel
            {
                Id = album.Id,
                Description = album.Description,
                Name = album.Name,
                LastAdded = album.tbl_Photo != null ? PhotoMapper.MapDetailsPhotoViewModel(PhotoBI.GetLastAddedInAlbum(album)) : null
            };
        
        }
    }
}
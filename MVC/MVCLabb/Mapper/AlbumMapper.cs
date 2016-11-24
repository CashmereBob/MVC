using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Models;
using MVCLabb.BI;
using MVCLabb.Mapper;

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

        internal static DetailAlbumViewModel MapDetailAlbumViewModel(tbl_Album album)
        {
            return new DetailAlbumViewModel
            {
                Comments = album.tbl_Comment != null ? CommentMapper.MapCommentViewModel(album.tbl_Comment) : null,
                Description = album.Description,
                id = album.Id,
                Name = album.Name,
                UserName = album.tbl_User.Name,
                Photos = PhotoMapper.MapIndexPhotoViewModel(album.tbl_Photo)

            };
        }
    }
}
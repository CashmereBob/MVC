using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Models;
using MVCLabb.Data;
using MVCLabb.Data.Repository;
using MVCLabb.Mapper;

namespace MVCLabb.Mapper
{
    public static class AlbumMapper
    {
        internal static AlbumViewModel MapAlbumViewModel(Album album, IPhotoRepository PhotoBI)
        {
            return new AlbumViewModel
            {
                Id = album.Id,
                Description = album.Description,
                Name = album.Name,
                LastAdded = album.Photos != null ? PhotoMapper.MapDetailsPhotoViewModel(PhotoBI.GetLastAddedInAlbum(album)) : null
            };
        
        }

        internal static DetailAlbumViewModel MapDetailAlbumViewModel(Album album)
        {
            return new DetailAlbumViewModel
            {
                Comments = album.Comments != null ? CommentMapper.MapCommentViewModel(album.Comments) : null,
                Description = album.Description,
                id = album.Id,
                Name = album.Name,
                UserName = album.User.Name,
                Photos = PhotoMapper.MapIndexPhotoViewModel(album.Photos)

            };
        }
    }
}
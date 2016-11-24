﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLabb.BI
{
    public static class AlbumBI
    {
        internal static List<tbl_Album> GettAllAlbumsByUserID(Guid userID)
        {
            using (var ctx = new MVCLabbEntities())
            {
               return ctx.tbl_Album.Include("tbl_User").Include("tbl_Photo").Where(x => x.tbl_User.Id == userID).ToList();
            }

        }

        internal static void AddAlbum(tbl_Album album)
        {
            using (var ctx = new MVCLabbEntities())
            {
                album.Id = Guid.NewGuid();
                ctx.tbl_Album.Add(album);

                ctx.SaveChanges();
            }
            
        }

        internal static List<tbl_Album> GettAllAlbums()
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Album.Include("tbl_User").Include("tbl_Photo").ToList();
            }
        }

        internal static tbl_Album GetAlbumByID(Guid id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Album.Include("tbl_User").Include("tbl_Photo").FirstOrDefault(x => x.Id == id);
            }
        }

        internal static void UpdateAlbum(tbl_Album album)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var albumDB = ctx.tbl_Album.FirstOrDefault(x => x.Id == album.Id);

                albumDB.Name = album.Name;
                albumDB.Description = album.Description;

                ctx.SaveChanges();
            }
        }

        internal static List<tbl_Album> GetSearchAlbumsFromDB(string search)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Album.Include("tbl_User").Include("tbl_Photo").Where(x =>
                x.Name.Contains(search) || x.Description.Contains(search) || x.tbl_User.Name.Contains(search)).ToList();
            }
        }

        internal static tbl_Album GetAlbumByID(string id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.tbl_Album.Include("tbl_User").Include("tbl_Photo").Include("tbl_Comment").FirstOrDefault(x => x.Id.ToString() == id);
            }
        }

        internal static void DeleteAlbum(tbl_Album album)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var albumDB = ctx.tbl_Album.FirstOrDefault(x => x.Id == album.Id);

                ctx.tbl_Album.Remove(albumDB);

                ctx.SaveChanges();
            }
        }
    }
}
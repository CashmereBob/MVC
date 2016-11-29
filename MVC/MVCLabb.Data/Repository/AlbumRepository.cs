using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        public void AddAlbum(Album album)
        {
            using (var ctx = new MVCLabbEntities())
            {
                album.Id = Guid.NewGuid();
                ctx.Albums.Add(album);

                ctx.SaveChanges();
            }
        }

        public void DeleteAlbum(Album album)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var albumDB = ctx.Albums.FirstOrDefault(x => x.Id == album.Id);

                ctx.Albums.Remove(albumDB);

                ctx.SaveChanges();
            }
        }

        public Album GetAlbumByID(string id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Albums.Include("User").Include("Photos").Include("Comments").FirstOrDefault(x => x.Id.ToString() == id);
            }
        }

        public Album GetAlbumByID(Guid id)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Albums.Include("User").Include("Photos").Include("Comments").FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Album> GetSearchAlbumsFromDB(string search)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Albums.Include("User").Include("Photos").Include("Comments").Where(x =>
                x.Name.Contains(search) || x.Description.Contains(search) || x.User.Name.Contains(search)).ToList();
            }
        }

        public List<Album> GettAllAlbums()
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Albums.Include("User").Include("Photos").Include("Comments").ToList();
            }
        }

        public List<Album> GettAllAlbumsByUserID(Guid userID)
        {
            using (var ctx = new MVCLabbEntities())
            {
                return ctx.Albums.Include("User").Include("Photos").Include("Comments").Where(x => x.User.Id == userID).ToList();
            }
        }

        public void UpdateAlbum(Album album)
        {
            using (var ctx = new MVCLabbEntities())
            {
                var albumDB = ctx.Albums.FirstOrDefault(x => x.Id == album.Id);

                albumDB.Name = album.Name;
                albumDB.Description = album.Description;

                ctx.SaveChanges();
            }
        }
    }
}

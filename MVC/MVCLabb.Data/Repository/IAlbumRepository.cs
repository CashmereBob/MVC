using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Repository
{
    public interface IAlbumRepository
    {
        List<Album> GettAllAlbumsByUserID(Guid userID);


        void AddAlbum(Album album);

        List<Album> GettAllAlbums();

        Album GetAlbumByID(Guid id);

        void UpdateAlbum(Album album);

        List<Album> GetSearchAlbumsFromDB(string search);

        Album GetAlbumByID(string id);

        void DeleteAlbum(Album album);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Repository
{
    public interface IPhotoRepository
    {
        Photo GetLastAddedInAlbum(Album album);

        List<Photo> GetPhotoFromDbByUserId(Guid userID);

        Photo GetLastAddedPhoto();

        List<Photo> GetSearchPhotosFromDb(string search);

        List<Photo> GetAllPhotosFromDb();

        List<Photo> GetAllPhotoesInAlbumByID(string id);

        Photo GetPhotoFromDbById(Guid id);

        void AddPhotoToDB(Photo photoToDB);

        void UdaptePhoto(Photo photo);

        void DeletePhotoFromDB(Photo model);
    }
}

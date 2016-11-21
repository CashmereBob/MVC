using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Models;
namespace MVCLabb.HelperMethods
{
    public static class PhotoHelper
    {
        public static List<string> GetListOfAlbumsFromListOfPhotos(IEnumerable<IndexPhotoViewModel> photoes)
        {
            var result = new List<string>();

            foreach (var photo in photoes)
            {
                if (!result.Contains(photo.Album))
                {
                    result.Add(photo.Album);
                }
            }

            return result;
        }
    }
}
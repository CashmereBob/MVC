using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLabb.Models
{
    public class AlbumViewModel
    {
        public Guid Id { get; set; }
        public DetailsPhotoViewModel LastAdded { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class DetailAlbumViewModel
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public ICollection<IndexPhotoViewModel> Photos { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }

        public DetailAlbumViewModel()
        {
            Photos = new HashSet<IndexPhotoViewModel>();
            Comments = new HashSet<CommentViewModel>();
        }
    }
}
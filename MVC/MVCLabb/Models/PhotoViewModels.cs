using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCLabb.Models
{
    public class DetailsPhotoViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string UploaderName { get; set; }
        public string Album { get; set; }
        public string Path { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public ICollection<CommentViewModel> Comments { get; set; }
        public DetailsPhotoViewModel()
        {
            Comments = new HashSet<CommentViewModel>();
        }

    }

    public class IndexPhotoViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public string Name { get; set; }
        public string Album { get; set; }

    }

    public class IndexViewModel
    {
        public int Photoes { get; set; }
        public int Users { get; set; }
        public int Comments { get; set; }
        public int Albums { get; set; }
        public IndexPhotoViewModel Latest { get; set; }
    }
}
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
    }
}
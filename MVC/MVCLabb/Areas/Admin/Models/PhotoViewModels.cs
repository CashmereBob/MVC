using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MVCLabb.Models;

namespace MVCLabb.Areas.Admin.Models
{
    public class CreatePhotoViewModels
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

    }

    public class DeletePhotoViewModels
    {
      
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Path { get; set; }

    }

    public class EditPhotoViewModels
    {
     
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        public string Path { get; set; }
     
        public string Description { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }

        public EditPhotoViewModels()
        {
            Comments = new HashSet<CommentViewModel>();
        }

    }

    public class IndexPhotoViewModels
    {
       
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public int Comments { get; set; }
    }
}
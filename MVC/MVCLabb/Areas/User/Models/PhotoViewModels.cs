﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MVCLabb.Models;
using System.Web.Mvc;

namespace MVCLabb.Areas.User.Models
{
    public class CreatePhotoViewModels
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public ICollection <SelectListItem> Albums { get; set; }
        public Guid AlbumId { get; set; }
        public CreatePhotoViewModels()
        {
            Albums = new HashSet<SelectListItem>();
        }


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
        public ICollection<SelectListItem> Albums { get; set; }
        public Guid AlbumId { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }

        public EditPhotoViewModels()
        {
            Comments = new HashSet<CommentViewModel>();
            Albums = new HashSet<SelectListItem>();
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
        public string Album { get; set; }
    }

   
}
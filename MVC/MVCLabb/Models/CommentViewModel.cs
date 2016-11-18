using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLabb.Models
{
    public class CommentViewModel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string comment { get; set; }
    }
}
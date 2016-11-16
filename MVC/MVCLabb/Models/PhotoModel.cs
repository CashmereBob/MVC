using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLabb.Models
{
    public class PhotoModel
    {

        public PhotoModel()
        {
            comments = new HashSet<CommentModel>();
        }

        public Guid id { get; set; }
        public string path { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ICollection<CommentModel> comments { get; set; }
    }
}
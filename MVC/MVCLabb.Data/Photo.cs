using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data
{
    public class Photo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Guid UserID { get; set; }
        public virtual User User { get; set; }
        public Guid? AlbumID { get; set; }
        public virtual Album Album { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Photo()
        {
            Comments = new HashSet<Comment>();
            Id = Guid.NewGuid();
        }    
    }
}

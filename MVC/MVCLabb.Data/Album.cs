using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data
{
    public class Album
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserID { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Album()
        {
            Comments = new HashSet<Comment>();
            Photos = new HashSet<Photo>();
            Id = Guid.NewGuid();
        }
    }
}

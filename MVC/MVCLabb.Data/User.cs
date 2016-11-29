using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool Admin { get; set; }
        public string Salt { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Album> Albums { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            Photos = new HashSet<Photo>();
            Comments = new HashSet<Comment>();
            Albums = new HashSet<Album>();
        }
    }
}

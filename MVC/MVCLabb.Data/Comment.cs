using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public Guid UserID { get; set; }
        public virtual User User { get; set; }
        public Guid? PhotoID { get; set; }
        public virtual Photo Photo { get; set; }
        public Guid? AlbumID { get; set; }
        public virtual Album Album { get; set; }
    }
}

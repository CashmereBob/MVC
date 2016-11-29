using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Repository
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment);

        List<Comment> GetAllComentsByPhotoID(string id);

        List<Comment> GetAllComments();

        void DeleteComment(Comment comment);

        Comment GetCommentByID(Guid id);
        

        List<Comment> GetAllComentsByAlbumID(string id);
    }
}

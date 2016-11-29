using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Models;
using MVCLabb.Data;
using MVCLabb.Data.Repository;

namespace MVCLabb.Mapper
{
    public class CommentMapper
    {
        internal static CommentViewModel MapCommentViewModel(Comment comment)
        {
            return new CommentViewModel
            {
                id = comment.Id,
                comment = comment.Content,
                date = comment.Date,
                email = comment.User.Email,
                name = comment.User.Name
            };
        }

        internal static Comment MapCommentViewModel(CommentViewModel comment)
        {
            return new Comment
            {
                Id = comment.id,
                Content = comment.comment,
                Date = comment.date,
                UserID = comment.userID
            };
        }

        internal static ICollection<CommentViewModel> MapCommentViewModel(ICollection<Comment> comments)
        {
            var result = new List<CommentViewModel>();

            comments.ToList().ForEach(x => result.Add(MapCommentViewModel(x)));

            return result;



        }
    }
}
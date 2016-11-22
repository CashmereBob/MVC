using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLabb.Models;

namespace MVCLabb.Mapper
{
    public class CommentMapper
    {
        internal static CommentViewModel MapCommentViewModel(tbl_Comment comment)
        {
            return new CommentViewModel
            {
                id = comment.Id,
                comment = comment.Comment,
                date = comment.Date,
                email = comment.tbl_User.Email,
                name = comment.tbl_User.Name
            };
        }

        internal static tbl_Comment MapCommentViewModel(CommentViewModel comment)
        {
            return new tbl_Comment
            {
                Id = comment.id,
                Comment = comment.comment,
                Date = comment.date,
                UserID = comment.userID
            };
        }
    }
}
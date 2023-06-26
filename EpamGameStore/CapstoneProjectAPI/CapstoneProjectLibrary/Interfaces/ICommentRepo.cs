using CapstoneProjectLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProjectLibrary.Interfaces
{
    public interface ICommentRepo
    {
        public int AddComment(Comment comment);
        public Task<List<Comment>> GetComments(int parentPostId);
        public Task<List<CommentReply>> GetReplies(int parentCommentId);
        public Comment EditComment(int commentId, string value);
        public CommentReply EditReply(int replyId, string value);
        public bool DeleteComment(int commentId);
        public bool DeleteReply(int replyId);
        public int AddReply(CommentReply reply);
    }
}

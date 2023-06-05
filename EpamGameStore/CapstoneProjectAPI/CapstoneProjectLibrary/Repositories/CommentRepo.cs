using CapstoneProjectLibrary.Interfaces;
using CapstoneProjectLibrary.Models;
using CapstoneProjectLibrary.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NLog.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProjectLibrary.Repositories
{
    public class CommentRepo: ICommentRepo
    {
        public EntityContext entityContext;

        public CommentRepo()
        {
            entityContext = new EntityContext();
        }

        public CommentRepo(EntityContext entityContext)
        {
            this.entityContext = entityContext;
        }


        public int AddComment(Comment comment)
        {

            var id = CheckCommentId(comment, entityContext);
         
            entityContext.Comments.Add(comment);
            entityContext.SaveChanges();
            return id;
        }

        private int CheckCommentId(Comment comment, EntityContext baseContext)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment), "Comment cannot be null");
            }

            if (comment.Id == 0)
            {
                var list = from i in baseContext.Comments
                           select i;
                int idFirstStep;
                var sortedList = list.OrderByDescending(i => i.Id);
                try
                {
                    idFirstStep = sortedList.First().Id;
                }
                catch
                {
                    idFirstStep = 0;
                }

                if (idFirstStep == 0)
                {
                    comment.Id = 1;
                }
                else
                {
                    comment.Id = idFirstStep + 1;
                }
                return comment.Id;
            }

            return comment.Id;
        }

        private int CheckReplyId(CommentReply reply, EntityContext baseContext)
        {
            if (reply == null)
            {
                throw new ArgumentNullException(nameof(reply), "Comment cannot be null");
            }

            if (reply.Id == 0)
            {
                var list = from i in baseContext.CommentReplies
                           select i;
                int idFirstStep;
                var sortedList = list.OrderByDescending(i => i.Id);
                try
                {
                    idFirstStep = sortedList.First().Id;
                }
                catch
                {
                    idFirstStep = 0;
                }

                if (idFirstStep == 0)
                {
                    reply.Id = 1;
                }
                else
                {
                    reply.Id = idFirstStep + 1;
                }
                return reply.Id;
            }

            return reply.Id;
        }

        public async Task<List<Comment>> GetComments(int parentPostId)
        {
            //Это исправляет ошибку при большом колличестве запросов
            using(var context = new EntityContext())
            {
                if (parentPostId == 0)
                {
                    throw new ArgumentException("Id error");
                }
                var list = await context.Comments.Where(c => c.ParentPostId == parentPostId).ToListAsync();
                return list;
            }          
        }

        public async  Task<List<CommentReply>> GetReplies(int parentCommentId)
        {
            //Это исправляет ошибку при большом колличестве запросов
            using (var context = new EntityContext())
            {
                if (parentCommentId == 0)
                {
                    throw new ArgumentException("Id error");
                }
                var list = await context.CommentReplies.Where(r => r.CommentId == parentCommentId).ToListAsync();
                return list;
            }          
        }

        public Comment EditComment(int commentId, string value)
        {
            if(commentId == 0)
            {
                throw new ArgumentException("Id cannot be zero");
            }
            var comment = entityContext.Comments.SingleOrDefault(c => c.Id == commentId);
            if(comment == null)
            {
                throw new ArgumentException("No comment with such id");
            }
            if(value != null)
            {
                comment.Value = value;
                comment.CreatedAt = DateTime.UtcNow;
                entityContext.SaveChanges();
            }
            return comment;

        }

        public CommentReply EditReply(int replyId, string value)
        {
            if (replyId == 0)
            {
                throw new ArgumentException("Id cannot be zero");
            }
            var commentReply = entityContext.CommentReplies.SingleOrDefault(c => c.Id == replyId);
            if (commentReply == null)
            {
                throw new ArgumentException("No comment with such id");
            }
            if (value != null)
            {
                commentReply.Value = value;
                commentReply.CreatedAt = DateTime.UtcNow;
                entityContext.SaveChanges();
            }
            return commentReply;
        }

        public bool DeleteComment(int commentId)
        {
            if(commentId == 0)
            {
                throw new ArgumentException("Id error");
            }
            var comment = entityContext.Comments.SingleOrDefault(c => c.Id == commentId);
            if (comment == null)
            {
                throw new ArgumentException("No comment with such id");
            }
            entityContext.Comments.Remove(comment);
            entityContext.SaveChanges();
            return true;
        }

        public bool DeleteReply(int replyId)
        {
            if (replyId == 0)
            {
                throw new ArgumentException("Id error");
            }
            var commentReply = entityContext.CommentReplies.SingleOrDefault(c => c.Id == replyId);
            if (commentReply == null)
            {
                throw new ArgumentException("No comment with such id");
            }
            entityContext.CommentReplies.Remove(commentReply);
            entityContext.SaveChanges();
            return true;
        }

        public int AddReply(CommentReply reply)
        {
            var id = CheckReplyId(reply, entityContext);
            entityContext.CommentReplies.Add(reply);
            entityContext.SaveChanges();
            return id;
        }
    }

}

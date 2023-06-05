using CapstoneProjectLibrary.Interfaces;
using CapstoneProjectLibrary.Repositories;
using CapstoneProjectLibrary;
using Microsoft.AspNetCore.Mvc;
using CapstoneProjectLibrary.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace CapstoneProjectAPI.Controllers
{
    public class CommentController : Controller
    {
        public ICommentRepo CommentRepo { get; set; }

        private EntityContext _context;
        public CommentController(ICommentRepo commentRepo)
        {
            CommentRepo = commentRepo;
            _context = ((CommentRepo)commentRepo).entityContext;
        }

        [HttpPost]
        [Route("api/comments/addcomment")]
        [Authorize]
        public int AddComment(int postId, string value)
        {
            var UserClaim = HttpContext.User;
            var email = UserClaim.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var user = _context.users.Where(u => u.Email == email).Select(u => new {
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                AvatarUrl = u.AvatarUrl
            }).FirstOrDefault();

            var newComment = new Comment();

            newComment.AuthorName = user.FirstName + " " + user.LastName;
            newComment.ParentPostId = postId;
            newComment.Value = value;
            var data = DateTime.UtcNow;         
            newComment.CreatedAt = data;

            var id = CommentRepo.AddComment(newComment);
            return id;
        }

        [HttpGet]
        [Route("api/comments/getcomments")]
        public async Task<IEnumerable<Comment>> GetComments(int parentPostId)
        {
           var comments = await CommentRepo.GetComments(parentPostId);
            return comments;
        }

        [HttpGet]
        [Route("api/comments/getreplys")]
        public async Task<IEnumerable<CommentReply>> GetReplys(int parentCommentId)
        {
            var replys = await CommentRepo.GetReplies(parentCommentId);
            return replys; 
        }

        [HttpPost]
        [Route("api/comments/editcomment")]
        public async Task<Comment> EditComment(int commentId, string newValue)
        {
            var comment =  CommentRepo.EditComment(commentId, newValue);
            return comment;
        }

        [HttpPost]
        [Route("api/comments/editreply")]
        public async Task<CommentReply> EditReply(int replyId, string newValue)
        {
            var commentReply = CommentRepo.EditReply(replyId, newValue);
            return commentReply;
        }

        [HttpPost]
        [Route("api/comments/deletecomment")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var val = CommentRepo.DeleteComment(commentId);
            if (val)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("api/comments/deletereply")]
        public async Task<IActionResult> DeleteReply(int replyId)
        {
            var val = CommentRepo.DeleteReply(replyId);
            if (val)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/comments/addreply")]
        public async Task<IActionResult> AddReply(int parentCommentId, string value)
        {
            var UserClaim = HttpContext.User;
            var email = UserClaim.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var user = _context.users.Where(u => u.Email == email).Select(u => new {
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                AvatarUrl = u.AvatarUrl
            }).FirstOrDefault();

            var newCommentReply = new CommentReply();

            newCommentReply.AuthorName = user.FirstName + " " + user.LastName;
            newCommentReply.CommentId = parentCommentId;
            newCommentReply.Value = value;
            var data = DateTime.UtcNow;
            newCommentReply.CreatedAt = data;

            var id = CommentRepo.AddReply(newCommentReply);
            var result = _context.CommentReplies.Where(r => r.Id == id);
            return Ok(result);
        }
    }
}

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
            newComment.AuthorEmail = email;
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
        [Authorize]
        public async Task<IActionResult> EditComment(int commentId, string newValue)
        {
            var editor = (ClaimsIdentity)User.Identity;
            var editorEmail = editor.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var role = editor.FindFirst(ClaimsIdentity.DefaultRoleClaimType).Value;

            if (role != null && (role == "Admin" || role == "Manager"))
            {
                var comment = CommentRepo.EditComment(commentId, newValue);
                return Ok(comment);
            }

            if(editorEmail != null)
            {
                var checkComment = _context.Comments.FirstOrDefault(com => com.Id == commentId);
                if(checkComment != null)
                {
                    if(editorEmail == checkComment.AuthorEmail)
                    {
                        var comment = CommentRepo.EditComment(commentId, newValue);
                        return Ok(comment);
                    }
                    else
                    {
                        return NotFound();
                    }
                    
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }                   
        }

        [HttpPost]
        [Route("api/comments/editreply")]
        [Authorize]
        public async Task<IActionResult> EditReply(int replyId, string newValue)
        {
            var editor = (ClaimsIdentity)User.Identity;
            var editorEmail = editor.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var role = editor.FindFirst(ClaimsIdentity.DefaultRoleClaimType).Value;

            if (role != null && (role == "Admin" || role == "Manager"))
            {
                var commentReply = CommentRepo.EditReply(replyId, newValue);
                return Ok(commentReply);
            }

            if (editorEmail != null)
            {
                var checkComment = _context.CommentReplies.FirstOrDefault(com => com.Id == replyId);
                if (checkComment != null)
                {
                    if (editorEmail == checkComment.AuthorEmail)
                    {
                        var commentReply = CommentRepo.EditReply(replyId, newValue);
                        return Ok(commentReply);
                    }
                    else
                    {
                        return NotFound();
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/comments/deletecomment")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var editor = (ClaimsIdentity)User.Identity;
            var editorEmail = editor.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var role = editor.FindFirst(ClaimsIdentity.DefaultRoleClaimType).Value;

            if (role != null && (role == "Admin" || role == "Manager"))
            {
                var val = CommentRepo.DeleteComment(commentId);
                return Ok(val);
            }

            if (editorEmail != null)
            {
                var checkComment = _context.Comments.FirstOrDefault(com => com.Id == commentId);
                if (checkComment != null)
                {
                    if (editorEmail == checkComment.AuthorEmail)
                    {
                        var val = CommentRepo.DeleteComment(commentId);
                        return Ok(val);
                    }
                    else
                    {
                        return NotFound();
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
            
           
        }
        [HttpPost]
        [Route("api/comments/deletereply")]
        public async Task<IActionResult> DeleteReply(int replyId)
        {
            var editor = (ClaimsIdentity)User.Identity;
            var editorEmail = editor.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var role = editor.FindFirst(ClaimsIdentity.DefaultRoleClaimType).Value;

            if (role != null && (role == "Admin" || role == "Manager"))
            {
                var val = CommentRepo.DeleteReply(replyId);
                return Ok(val);
            }

            if (editorEmail != null)
            {
                var checkComment = _context.CommentReplies.FirstOrDefault(com => com.Id == replyId);
                if (checkComment != null)
                {
                    if (editorEmail == checkComment.AuthorEmail)
                    {
                        var val = CommentRepo.DeleteReply(replyId);
                        return Ok(val);
                    }
                    else
                    {
                        return NotFound();
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/comments/addreply")]
        [Authorize]
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
            newCommentReply.AuthorName = email;
            newCommentReply.Value = value;
            var data = DateTime.UtcNow;
            newCommentReply.CreatedAt = data;

            var id = CommentRepo.AddReply(newCommentReply);
            var result = _context.CommentReplies.Where(r => r.Id == id);
            return Ok(result);
        }
    }
}

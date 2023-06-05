using System;
using System.Collections;
using System.Collections.Generic;

namespace CapstoneProjectLibrary.Models;

public class Comment
{
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public int ParentPostId { get; set; }
    public string Value { get; set; }   
    public DateTime CreatedAt { get; set; }
    public virtual ICollection<CommentReply> Replyes { get; set;}
    
    
}
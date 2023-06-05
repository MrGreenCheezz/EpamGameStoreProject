using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProjectLibrary.Models
{
    public class CommentReply
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
    }
}

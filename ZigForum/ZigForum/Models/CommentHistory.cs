using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigForum.Models
{
    public class CommentHistory
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }

        public virtual Comment Comment { get; set; }
    }
}

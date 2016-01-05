using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigForum.Models
{
    public class CommentVote
    {   
        public int CommentId { get; set; }
        public string UserAuthorId { get; set; }
        public string UserTargetId { get; set; }
        public bool IsUpVote { get; set; }
        public DateTime Created { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual User UserAuthor { get; set; }
        public virtual User UserTarget { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigForum.Models
{
    public class PostVote
    {   
        public int PostId { get; set; }
        public string UserAuthorId { get; set; }
        public string UserTargetId { get; set; }
        public bool IsUpVote { get; set; }
        public DateTime Created { get; set; }

        public virtual Post Post { get; set; }
        public virtual User UserAuthor { get; set; }
        public virtual User UserTarget { get; set; }
    }
}

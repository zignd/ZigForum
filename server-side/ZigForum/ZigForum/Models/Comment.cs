using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigForum.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public int? ParentId { get; set; }
        public string Body { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }

        public virtual Post Post { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual ICollection<Comment> Children { get; set; }
    }
}

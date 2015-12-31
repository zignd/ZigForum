using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigForum.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsLocked { get; set; }
        public string LockedReason { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }

        public virtual User User { get; set; }
    }
}

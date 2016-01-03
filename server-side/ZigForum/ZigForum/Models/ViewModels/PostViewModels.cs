using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigForum.Models.ViewModels
{
    public class PostGetViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsLocked { get; set; }
        public string LockedReason { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }

        public User User { get; set; }
        public Forum Forum { get; set; }
    }

    public class PostPostViewModel
    {
        public int ForumId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    public class PostPutViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ForumId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsLocked { get; set; }
        public string LockedReason { get; set; }
        public bool IsDeleted { get; set; }
    }
}

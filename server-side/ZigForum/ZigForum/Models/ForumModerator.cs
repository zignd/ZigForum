using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigForum.Models
{
    public class ForumModerator
    {
        public int UserId { get; set; }
        public int ForumId { get; set; }
        public DateTime Created { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigForum.Models
{
    public class ForumModerator
    {
        public string UserId { get; set; }
        public int ForumId { get; set; }
        public DateTime Created { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        
        public virtual Forum Forum { get; set; }
    }
}

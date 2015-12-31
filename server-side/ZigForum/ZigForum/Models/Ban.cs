using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigForum.Models
{
    public class Ban
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool IsBanned { get; set; }
        public string Reason { get; set; }
        public DateTime? BannedUntil { get; set; }
        public DateTime Created { get; set; }

        public virtual User User { get; set; }
    }
}

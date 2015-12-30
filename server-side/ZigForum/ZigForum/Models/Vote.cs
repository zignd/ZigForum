using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigForum.Models
{
    public class Vote
    {
        public int SubmissionTypeId { get; set; }
        public int SubmissionId { get; set; }
        public int UserAuthorId { get; set; }
        public int UserTargetId { get; set; }
        public bool IsUpVote { get; set; }
        public DateTime Created { get; set; }
    }
}

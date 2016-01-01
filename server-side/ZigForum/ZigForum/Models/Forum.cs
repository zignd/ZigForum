using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigForum.Models.Validators;

namespace ZigForum.Models
{
    public class Forum
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }

        public virtual Forum Parent { get; set; }
    }
}

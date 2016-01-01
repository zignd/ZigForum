using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigForum.Models.Validators;

namespace ZigForum.Models.ViewModels
{
    public class ForumGetViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }

        public Forum Parent { get; set; }
    }

    [Validator(typeof(ForumPostValidator))]
    public class ForumPostViewModel
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
    }

    [Validator(typeof(ForumPutValidator))]
    public class ForumPutViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
}

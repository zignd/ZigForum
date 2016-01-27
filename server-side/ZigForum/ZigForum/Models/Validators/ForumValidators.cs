using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigForum.Models.DTOs;

namespace ZigForum.Models.Validators
{
    public class ForumCreateNewValidator : AbstractValidator<ForumDTO>
    {
        public ForumCreateNewValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }

    public class ForumEditValidator : AbstractValidator<ForumDTO>
    {
        public ForumEditValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}

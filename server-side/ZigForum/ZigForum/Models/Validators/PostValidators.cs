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
    public class PostCreateNewValidator : AbstractValidator<PostDTO>
    {
        public PostCreateNewValidator()
        {
            RuleFor(x => x.ForumId)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty();

            RuleFor(x => x.Body)
                .NotEmpty();
        }
    }

    public class PostEditValidator : AbstractValidator<PostDTO>
    {
        public PostEditValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty();

            RuleFor(x => x.Body)
                .NotEmpty();
        }
    }
}

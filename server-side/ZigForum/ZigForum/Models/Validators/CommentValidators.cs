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
    public class CommentCreateNewValidator : AbstractValidator<CommentDTO>
    {
        public CommentCreateNewValidator()
        {
            RuleFor(x => x.PostId)
                .NotEmpty();

            RuleFor(x => x.Body)
                .NotEmpty();
        }
    }

    public class CommentEditValidator : AbstractValidator<CommentDTO>
    {
        public CommentEditValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Body)
                .NotEmpty();
        }
    }
}

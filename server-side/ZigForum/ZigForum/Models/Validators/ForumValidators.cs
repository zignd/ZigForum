using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigForum.Models.ViewModels;

namespace ZigForum.Models.Validators
{
    //public class ForumGetValidator : AbstractValidator<Forum>
    //{
    //    public ForumValidators()
    //    {
    //        RuleSet("Put", () =>
    //        {
    //            RuleFor(x => x.Id)
    //                .NotNull();
    //        });

    //        RuleFor(x => x.Name)
    //            .NotNull();
    //    }
    //}

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

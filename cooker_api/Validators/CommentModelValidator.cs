using cooker_api.Models;
using FluentValidation;

namespace cooker_api.Validators
{
    public class CommentModelValidator : AbstractValidator<CommentModel>
    {
        public CommentModelValidator()
        {
            RuleFor(p => p.UserId)
                .NotNull().WithMessage("User ID field must not be empty.");
            RuleFor(p => p.PostId)
                .NotNull().WithMessage("Post ID field must not be empty.");
            RuleFor(p => p.Content)
                .NotNull().WithMessage("Content field must not be empty.");
            RuleFor(p => p.LikeAmount)
                .NotNull().WithMessage("Like amount field must not be empty.");
        }
    }
}

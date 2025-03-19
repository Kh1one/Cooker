using cooker_api.Models;
using FluentValidation;

public class FavouriteModelValidator : AbstractValidator<FavouriteModel>
{
    public FavouriteModelValidator()
    {
        RuleFor(p => p.UserId)
            .NotNull().WithMessage("UserId field must no be empty.");
        RuleFor(p => p.PostId)
            .NotNull().WithMessage("PostId field must no be empty.");
    }
}

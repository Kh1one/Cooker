using cooker_api.Models;
using FluentValidation;
public class PostModelValidator: AbstractValidator<PostModel>
{
    public PostModelValidator()
    {
        RuleFor(p => p.UserId)
            .NotNull().WithMessage("UserId field must no be empty.");
        RuleFor(p => p.FavouriteAmount)
            .NotNull().WithMessage("FavouriteAmount field must no be empty.");
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Title field must no be empty.");

        RuleFor(p => p.Ingridients)
            .NotEmpty().WithMessage("Ingridients field must no be empty.");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description field must no be empty.");

        RuleFor(p => p.Instructions)
            .NotEmpty().WithMessage("Instructions field must no be empty.");
    }
}
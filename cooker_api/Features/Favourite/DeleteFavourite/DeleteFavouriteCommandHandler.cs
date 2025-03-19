using cooker_api.entities;
using cooker_api.Features.Post.InsertPost;
using cooker_api.Models;
using cooker_api.Services;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Features.Favourite.DeleteFavourite
{
    public class DeleteFavouriteCommandHandler : IRequestHandler<DeleteFavouriteCommand, ResponseModel<FavouriteModel>>
    {
        private readonly CookerContext _db;
        private readonly IValidator<FavouriteModel> _registerValidator;

        public DeleteFavouriteCommandHandler(CookerContext db, IValidator<FavouriteModel> registerValidator)
        {
            _db = db;
            _registerValidator = registerValidator;
        }

        public async Task<ResponseModel<FavouriteModel>> Handle(DeleteFavouriteCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _registerValidator.ValidateAsync(request.Request);
            if (validationResult.IsValid == false)
            {
                return new ResponseModel<FavouriteModel>
                {
                    Data = null,
                    Title = "Invalid model state",
                    Status = 400,
                    //Detail = validationResult.Errors.Select(e => e.ErrorMessage).ToList().ToString(),
                    Detail = "Validation failed: " + string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    Instance = ""
                    //HttpContext.Request.Path
                };
            }

            //deleting entry in favourite table
            var entity = await _db.Favourites
                .Where(Q => Q.UserId == request.Request.UserId && Q.PostId == request.Request.PostId)
                .FirstOrDefaultAsync();
            _db.Favourites.Remove(entity!);
            await _db.SaveChangesAsync();

            //decrement post's favourite count
            var existingPost = await _db.Posts
                .Where(Q => Q.PostId == request.Request.PostId)
                .FirstOrDefaultAsync();

            existingPost!.FavouriteAmount--;
            _db.Posts.Update(existingPost);
            await _db.SaveChangesAsync();


            //get post's creator and decrement their favourite count
            var postCreator = await _db.Users
                .Where(Q => Q.UserId == existingPost.UserId)
                .FirstOrDefaultAsync();

            postCreator!.FavouriteAmount--;
            _db.Posts.Update(existingPost);
            await _db.SaveChangesAsync();


            return new ResponseModel<FavouriteModel>
            {
                Data = null,
                Title = "Favourite fetching successful.",
                Status = 201,
                Detail = "Entry fetched from database.",
                Instance = ""
            };
        }
    }
}

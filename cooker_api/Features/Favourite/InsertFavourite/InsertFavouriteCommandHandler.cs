using cooker_api.entities;
using cooker_api.Features.Post.InsertPost;
using cooker_api.Models;
using cooker_api.Services;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Features.Favourite.InsertFavourite
{
    public class InsertFavouriteCommandHandler : IRequestHandler<InsertFavouriteCommand, ResponseModel<FavouriteModel>>
    {
        private readonly CookerContext _db;
        private readonly IValidator<FavouriteModel> _registerValidator;

        public InsertFavouriteCommandHandler(CookerContext db, IValidator<FavouriteModel> registerValidator)
        {
            _db = db;
            _registerValidator = registerValidator;
        }

        public async Task<ResponseModel<FavouriteModel>> Handle(InsertFavouriteCommand request, CancellationToken cancellationToken)
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
            //check if post and user exists
            var existingPost = await _db.Posts
                .Where(Q => Q.PostId == request.Request.PostId)
                .FirstOrDefaultAsync();

            var existingUser = await _db.Users
                .Where(Q => Q.UserId == request.Request.UserId)
                .FirstOrDefaultAsync();

            if (existingUser == null)
            {
                return new ResponseModel<FavouriteModel>
                {
                    Data = null,
                    Title = "User not found.",
                    Status = 422,
                    Detail = $"User ID of {request.Request.UserId} does not exist in database.",
                    Instance = ""
                };
            }
            else if (existingPost == null)
            {
                return new ResponseModel<FavouriteModel>
                {
                    Data = null,
                    Title = "Post not found.",
                    Status = 422,
                    Detail = $"Post ID of {request.Request.PostId} does not exist in database.",
                    Instance = ""
                };
            }
            else
            {
                //creating new entry in favourite table
                var entity = new entities.Favourite
                {
                    UserId = request.Request.UserId,
                    PostId = request.Request.PostId,
                };

                _db.Favourites.Add(entity);
                await _db.SaveChangesAsync();

                //increment post's favourite count
                existingPost.FavouriteAmount++;
                _db.Posts.Update(existingPost);
                await _db.SaveChangesAsync();


                //get post's creator and increment their favourite count
                var postCreator = await _db.Users
                    .Where(Q => Q.UserId == existingPost.UserId)
                    .FirstOrDefaultAsync();

                postCreator!.FavouriteAmount++;
                _db.Posts.Update(existingPost);
                await _db.SaveChangesAsync();


                return new ResponseModel<FavouriteModel>
                {
                    Data = null,
                    Title = "Favourite insert successful.",
                    Status = 201,
                    Detail = "New favourite entry created in database.",
                    Instance = ""
                };
            }
        }
    }
}

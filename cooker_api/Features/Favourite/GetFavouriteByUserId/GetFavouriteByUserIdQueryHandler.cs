using cooker_api.entities;
using cooker_api.Features.Favourite.InsertFavourite;
using cooker_api.Models;
using cooker_api.Services;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace cooker_api.Features.Favourite.GetFavouriteByUserId
{
    public class GetFavouriteByUserIdQueryHandler : IRequestHandler<GetFavouriteByUserIdQuery, ResponseModel<List<PostModel>>>
    {
        private readonly CookerContext _db;

        public GetFavouriteByUserIdQueryHandler(CookerContext db)
        {
            _db = db;
        }

        public async Task<ResponseModel<List<PostModel>>> Handle(GetFavouriteByUserIdQuery request, CancellationToken cancellationToken)
        {
            var favouritePosts = await _db.Favourites
             .Where(fav => fav.UserId == request.UserId)
             .Join(_db.Posts,
                 fav => fav.PostId,
                 post => post.PostId,
                 (fav, post) => new { fav, post }) // Anonymous type to hold intermediate results
             .Join(_db.Users,
                 combined => combined.post.UserId, // Join on UserId from Post
                 user => user.UserId,
                 (combined, user) => new PostModel
                 {
                     PostId = combined.post.PostId,
                     UserId = combined.post.UserId,
                     Description = combined.post.Description,
                     Ingridients = combined.post.Ingridients,
                     Title = combined.post.Title,
                     Instructions = combined.post.Instructions,
                     FavouriteAmount = combined.post.FavouriteAmount,
                     HeaderPicture = combined.post.HeaderPicture,
                     Username = user.Username, 
                     UserProfilePicture = user.ProfilePicture 
                 })
             .ToListAsync();


            return new ResponseModel<List<PostModel>>
            {
                Data = favouritePosts.Any() ? favouritePosts : null,
                Title = favouritePosts.Any() ? "Favourites found." : "No favourites found.",
                Status = favouritePosts.Any() ? 200 : 404,
                Detail = favouritePosts.Any() ? "Favourite posts retrieved successfully." : $"No favourite posts found for User ID {request.UserId}.",
                Instance = ""
            };
        }
    }
}

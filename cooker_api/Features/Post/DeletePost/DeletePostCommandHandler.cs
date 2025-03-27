using cooker_api.Features.Post.InsertPost;
using cooker_api.Models;
using FluentValidation;
using MediatR;
using cooker_api.entities;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Features.Post.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, ResponseModel<PostModel>>
    {
        private readonly CookerContext _db;

        public DeletePostCommandHandler(CookerContext db)
        {
            _db = db;
        }

        public async Task<ResponseModel<PostModel>> Handle(DeletePostCommand requestId, CancellationToken cancellationToken)
        {
            var favouritesRemoved = 0;
            var existingData = await _db.Posts
                .Where(Q => Q.PostId == requestId.RequestId)
                .FirstOrDefaultAsync();

            if (existingData == null)
            {
                //no data
                return new ResponseModel<PostModel>
                {
                    Data = null,
                    Title = "Post not found.",
                    Status = 422,
                    Detail = $"Post with ID of {requestId.RequestId} does not exist in database.",
                    Instance = ""
                };
            }
            else
            {
                var existingFavourites = await _db.Favourites
                    .Where(Q => Q.PostId == existingData.PostId)
                    .ToListAsync();

                foreach( var fav in existingFavourites)
                {
                    _db.Favourites.Remove(fav);
                    await _db.SaveChangesAsync();
                    favouritesRemoved++;
                }


                _db.Posts.Remove(existingData);
                await _db.SaveChangesAsync();

                //reduce number of posts from user's data
                var userData = await _db.Users
                .Where(Q => Q.UserId == existingData.UserId)
                .FirstOrDefaultAsync();

                if (userData != null)
                {
                    userData.PostAmount--;
                    userData.FavouriteAmount -= favouritesRemoved;

                    _db.Users.Update(userData);
                    await _db.SaveChangesAsync();
                }

                return new ResponseModel<PostModel>
                {
                    Data = null,
                    Title = "Post deleted successfully.",
                    Status = 204,
                    Detail = "The post has been removed from the database.",
                    Instance = ""
                };
            }
        }
    }
}

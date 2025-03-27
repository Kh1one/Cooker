using cooker_api.entities;
using cooker_api.Features.Post.GetPostByUserId;
using cooker_api.Models;
using cooker_api.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Features.Post.GetPostByPostId
{
    public class GetPostByPostIdQueryHandler : IRequestHandler<GetPostByPostIdQuery, ResponseModel<PostModel>>
    {
        private readonly CookerContext _db;

        public GetPostByPostIdQueryHandler(CookerContext db)
        {
            _db = db;

        }

        public async Task<ResponseModel<PostModel>> Handle(GetPostByPostIdQuery postId, CancellationToken cancellationToken)
        {
            var results = await _db.Posts
                .Where(Q => Q.PostId == postId.PostId)
                .Join(_db.Users, 
                     p => p.UserId,
                     u => u.UserId,
                     (p, u) => new PostModel
                     {
                        PostId = p.PostId,
                        UserId = p.UserId,
                        Username = u.Username,
                        UserProfilePicture = u.ProfilePicture,
                        Title = p.Title,
                        Description = p.Description,
                        Ingridients = p.Ingridients,
                        Instructions = p.Instructions,
                        FavouriteAmount = p.FavouriteAmount,
                        HeaderPicture = p.HeaderPicture
                     })
                .FirstOrDefaultAsync();

                          


            if (results == null)
            {
                return new ResponseModel<PostModel>
                {
                    Data = null,
                    Title = "Posts not found.",
                    Status = 422,
                    Detail = $"Post ID of {postId.PostId} may not exist in database.",
                    Instance = ""
                };
            }
            else
            {
                return new ResponseModel<PostModel>
                {
                    Data = results,
                    Title = "Posts found.",
                    Status = 200,
                    Detail = "Posts found in database",
                    Instance = ""
                };
            }

        }
    }
}

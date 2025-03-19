using cooker_api.entities;
using cooker_api.Features.Post.DeletePost;
using cooker_api.Models;
using cooker_api.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace cooker_api.Features.Post.GetPostByUserId
{
    public class GetPostByUserIdQueryhandler : IRequestHandler<GetPostByUserIdQuery, ResponseModel<List<PostModel>>>
    {
        private readonly PostService _services;
        private readonly CookerContext _db;

        public GetPostByUserIdQueryhandler(PostService services, CookerContext db)
        {
            _services = services;
            _db = db;

        }

        public async Task<ResponseModel<List<PostModel>>> Handle(GetPostByUserIdQuery userId, CancellationToken cancellationToken)
        {
            var results = await _db.Posts
                .Where(Q => Q.UserId == userId.UserId)
                .Select(Q => new PostModel
                {
                    PostId = Q.PostId,
                    UserId = Q.UserId,
                    Title = Q.Title,
                    Description = Q.Description,
                    Ingridients = Q.Ingridients,
                    Instructions = Q.Instructions,
                    FavouriteAmount = Q.FavouriteAmount,
                    HeaderPicture = Q.HeaderPicture
                })
                .ToListAsync();

           
            return new ResponseModel<List<PostModel>>
            {
                Data = results.Any() ? results : null,
                Title = results.Any() ? "Posts found." : "Posts not found.",
                Status = results.Any() ? 200 : 422,
                Detail = results.Any() ? "Posts found in database" : $"User ID of {userId} may not exist in database.",
                Instance = ""
            };
            
        }
    }
}

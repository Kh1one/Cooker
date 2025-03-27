using cooker_api.entities;
using cooker_api.Features.Post.GetPostByUserId;
using cooker_api.Models;
using cooker_api.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Features.Post.GetAllPosts
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, ResponseModel<List<PostModel>>>
    {
        private readonly CookerContext _db;

        public GetAllPostsQueryHandler(CookerContext db)
        {
            _db = db;
        }

        public async Task<ResponseModel<List<PostModel>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var results = await _db.Posts
                .OrderByDescending(Q => Q.FavouriteAmount)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
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
                .ToListAsync();


            return new ResponseModel<List<PostModel>>
            {
                Data = results.Any() ? results : null,
                Title = results.Any() ? "Posts fetched." : "No posts found.",
                Status = results.Any() ? 200 : 404,
                Detail = results.Any() ? "Posts soccessfully fetched from database" : "No posts exist in database.",
                Instance = ""
            };

        }
    }
}

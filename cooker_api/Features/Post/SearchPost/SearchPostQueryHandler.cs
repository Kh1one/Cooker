using cooker_api.entities;
using cooker_api.Features.Post.GetAllPosts;
using cooker_api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Features.Post.SearchPost
{
    public class SearchPostQueryHandler : IRequestHandler<SearchPostQuery, ResponseModel<List<PostModel>>>
    {
        private readonly CookerContext _db;

        public SearchPostQueryHandler(CookerContext db)
        {
            _db = db;
        }

        public async Task<ResponseModel<List<PostModel>>> Handle(SearchPostQuery request, CancellationToken cancellationToken)
        {
            var results = await _db.Posts
                .Where(Q => 
                    Q.Title.Contains(request.SearchTerm) ||
                    Q.Description.Contains(request.SearchTerm))
                .OrderByDescending(Q => Q.FavouriteAmount)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Join(_db.Users,
                    p => p.UserId,
                    u => u.UserId,
                    (u, p) => new PostModel
                    {
                        PostId = u.PostId,
                        UserId = u.UserId,
                        Title = u.Title,
                        Description = u.Description,
                        Ingridients = u.Ingridients,
                        Instructions = u.Instructions,
                        FavouriteAmount = u.FavouriteAmount,
                        HeaderPicture = u.HeaderPicture,
                        UserProfilePicture = p.ProfilePicture,
                        Username = p.Username,
                    })
                .ToListAsync();


            return new ResponseModel<List<PostModel>>
            {
                Data = results.Any() ? results : null,
                Title = results.Any() ? "Posts fetched." : "No posts found.",
                Status = results.Any() ? 200 : 404,
                Detail = results.Any() ? "Posts soccessfully fetched from database" : $"No posts relevant to \"{request.SearchTerm}\" exists.",
                Instance = ""
            };

        }
    }
}

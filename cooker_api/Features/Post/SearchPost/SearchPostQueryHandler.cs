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
                Title = results.Any() ? "Posts fetched." : "No posts found.",
                Status = results.Any() ? 200 : 404,
                Detail = results.Any() ? "Posts soccessfully fetched from database" : $"No posts relevant to \"{request.SearchTerm}\" exists.",
                Instance = ""
            };

        }
    }
}

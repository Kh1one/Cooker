using cooker_api.entities;
using cooker_api.Features.Favourite.GetFavouriteByUserId;
using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace cooker_api.Features.Comment.GetCommentByPostId
{
    public class GetCommentByPostIdQueryHandler : IRequestHandler<GetCommentByPostIdQuery, ResponseModel<List<CommentModel>>>
    {
        private readonly CookerContext _db;
        public GetCommentByPostIdQueryHandler(CookerContext db)
        {
            _db = db;
        }

        public async Task<ResponseModel<List<CommentModel>>> Handle(GetCommentByPostIdQuery request, CancellationToken cancellationToken)
        {
            var postComments = await _db.Comments
                .Where(Q => Q.PostId == request.PostId)
                .Join(_db.Users,
                    c => c.UserId,
                    u => u.UserId,
                    (c, u) => new CommentModel
                    {
                        CommentId = c.CommentId,
                        PostId = c.PostId,
                        UserId = c.UserId,
                        Username = u.Username,
                        ProfilePicture = u.ProfilePicture,
                        Content = c.Content,
                        LikeAmount = c.LikeAmount,
                        Edited = c.Edited
                    })
                .OrderByDescending(Q => Q.LikeAmount)
                .ToListAsync();

            return new ResponseModel<List<CommentModel>>
            {
                Data = postComments.Any() ? postComments: null,
                Title = postComments.Any() ? "Comments retrieved successfully." : "No comments found.",
                Status = postComments.Any() ? 200 : 404,
                Detail = postComments.Any() ? "Comments found in database." : $"No comments found for the Post ID {request.PostId}.",
                Instance = ""
            };
        }
    } 
}

